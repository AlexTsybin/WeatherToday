using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Support.V4.App;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Views;
using WeatherToday.Android.Activity;
using WeatherToday.Android.Views;
using WeatherToday.Core.Services.Platform;
using WeatherToday.Localization;

namespace WeatherToday.Android.Presenter
{
    public class WeatherAndroidPresenter : MvxAppCompatViewPresenter, ICustomPresenter
    {
        //protected override void PerformShowFragmentTransaction(FragmentManager fragmentManager, MvxFragmentPresentationAttribute attribute, MvxViewModelRequest request)
        //{
        //    BaseVM viewModel = null;

        //    // MvxNavigationService provides an already instantiated ViewModel here
        //    if (request is MvxViewModelInstanceRequest instanceRequest)
        //        viewModel = instanceRequest.ViewModelInstance as BaseVM;

        //    if (viewModel == null)
        //    {
        //        //(CurrentActivity as CommonActivity).ShowToast(Strings.message_error_default);
        //        return;
        //    }

        //    // Имя фрагмента в стеке генерится на основании типа VM и хэш-кода объекта (в идеале нужно придумать более надежный способ идентификации TODO).
        //    string backStackName = viewModel.GetUniqName();

        //    string fragmentName = attribute.ViewType.FragmentJavaName();

        //    IMvxFragmentView fragment = null;
        //    if (attribute.IsCacheableFragment)
        //    {
        //        backStackName = attribute.Tag ?? backStackName;
        //        fragment = (IMvxFragmentView)fragmentManager.FindFragmentByTag(backStackName);
        //    }

        //    fragment = fragment ?? CreateFragment(attribute, fragmentName);

        //    // Если фрагмент был взят из кэша, мы сравниваем его VM с новой VM и в случае совпадения оставляем VM фрагмента неизменной.
        //    if (fragment.ViewModel == null || !fragment.ViewModel.Equals(viewModel))
        //    {
        //        if (fragment.ViewModel is BaseVM baseVM)
        //        {
        //            baseVM.Unbind();
        //            baseVM.DisposeIfDisposable();
        //        }

        //        fragment.ViewModel = viewModel;
        //    }
        //    else if (fragment.ViewModel.Equals(viewModel))
        //    {
        //        // У VM уже был вызван Prepare, где произошла основная подписка на токены VM. Если подписка находится в Initialize- переделать!
        //        // Эта VM нам не нужна, соответственно мы отписываемся от токенов.
        //        viewModel.Unbind();
        //        viewModel.DisposeIfDisposable();
        //    }

        //    // save MvxViewModelRequest in the Fragment's Arguments
        //    Bundle bundle = new Bundle();
        //    string serializedRequest = NavigationSerializer.Serializer.SerializeObject(request);
        //    bundle.PutString(ViewModelRequestBundleKey, serializedRequest);

        //    if (fragment is BaseFragment fragmentView)
        //    {
        //        if (fragmentView.Arguments == null)
        //        {
        //            fragmentView.Arguments = bundle;
        //        }
        //        else
        //        {
        //            fragmentView.Arguments.Clear();
        //            fragmentView.Arguments.PutAll(bundle);
        //        }
        //    }

        //    AddNewFragmentToActivity(fragmentManager, fragment, attribute, request, backStackName);
        //}

        public WeatherAndroidPresenter(IEnumerable<Assembly> androidViewAssemblies)
            : base(androidViewAssemblies)
        {
        }

        public bool CanPop(FragmentManager fragmentManager = null)
        {
            if (fragmentManager == null)
                return false;

            if (CurrentActivity is MainActivity)
                return fragmentManager.BackStackEntryCount > 1;
            else
                return fragmentManager.BackStackEntryCount > 0;
        }

        public async void MoveBack(FragmentManager fragmentManager)
        {
            if (CanPop(fragmentManager))
            {
                Fragment lastFragment = fragmentManager.Fragments.LastOrDefault();

                if (lastFragment is IMvxView mvxFragment)
                    await Close(mvxFragment.ViewModel);
            }
            else if (CurrentActivity is IBackHandler backHandledActivity)
            {
                if (await backHandledActivity.BackPressed())
                    return;
                else if (CurrentActivity is CommonActivity commonActivity)
                    await Close(commonActivity.ViewModel);
                else
                    CurrentActivity?.Finish();
            }
            else if (CurrentActivity is MainActivity)
                Mvx.IoCProvider.Resolve<IUserInteraction>().ShowCustomNotification(Strings.message_close_app, 3000, Strings.yes, () =>
                {
                    Mvx.IoCProvider.Resolve<IUserInteraction>().CloseApp();
                });
            else if (CurrentActivity != null && CurrentActivity is CommonActivity commonActivity)
                await Close(commonActivity.ViewModel);
            else if (CurrentActivity != null)
                CurrentActivity.Finish();
        }

        public MvxFragment GetTopFragment()
        {
            return CurrentFragmentManager?.Fragments?.LastOrDefault() as MvxFragment;
        }

        public void DeleteCurrentFragmentFromActivity(FragmentManager fragmentManager, IMvxFragmentView fragmentView)
        {
            MvxFragmentPresentationAttribute attribute = (MvxFragmentPresentationAttribute)Attribute.GetCustomAttribute(fragmentView.GetType(), typeof(MvxFragmentPresentationAttribute));
            TryPerformCloseFragmentTransaction(fragmentManager, attribute);
        }

        //public void AddNewFragmentToActivity(FragmentManager fragmentManager, IMvxFragmentView fragment,
        //    MvxFragmentPresentationAttribute attribute, MvxViewModelRequest request, string backStackName)
        //{
        //    // обновляем закэшированную вьюшку для телефона.
        //    if (attribute.IsCacheableFragment && !string.IsNullOrWhiteSpace(attribute.Tag) &&
        //        fragment is BaseFragment baseFragment && baseFragment.View != null)
        //        baseFragment.ReloadFragment();

        //    var fragmentView = fragment.ToFragment();

        //    var ft = fragmentManager.BeginTransaction();

        //    OnBeforeFragmentChanging(ft, fragmentView, attribute, request);

        //    if (attribute.AddToBackStack)
        //        ft.AddToBackStack(backStackName);
        //    else
        //    {
        //        fragmentManager.PopBackStackImmediate(null, FragmentManager.PopBackStackInclusive);
        //        ft.AddToBackStack(backStackName); // В нашем случае фрагменты всегда добавляются в стек.
        //    }

        //    OnFragmentChanging(ft, fragmentView, attribute, request);

        //    ft.Replace(attribute.FragmentContentId, (Fragment)fragment, backStackName);
        //    ft.CommitAllowingStateLoss();

        //    OnFragmentChanged(ft, fragmentView, attribute, request);
        //}
    }
}