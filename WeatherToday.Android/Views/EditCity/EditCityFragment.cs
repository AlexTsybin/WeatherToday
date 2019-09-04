using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.EditCity;
using WeatherToday.Localization;

namespace WeatherToday.Android.Views.EditCity
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(EditCityFragment))]
    public class EditCityFragment : BaseFragment<EditCityVM>
    {
        protected override int FragmentId => Resource.Layout.page_edit_city;

        #region Public

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ParentActivity.SupportActionBar.Title = Strings.edit_city_title;
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ParentActivity.SupportActionBar.SetHomeButtonEnabled(true);

            return view;
        }

        public override void OnPause()
        {
            base.OnPause();

            MainActivity.HideSoftKeyboard();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_edit_city, menu);

            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_save_city:
                    ViewModel.SaveCityCommand.Execute();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        #endregion
    }
}