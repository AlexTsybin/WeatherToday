using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Refractored.Fab;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.CityList;
using WeatherToday.Localization;

namespace WeatherToday.Android.Views.CityList
{
    [MvxFragmentPresentation(typeof(MainVM), Resource.Id.content_frame, true)]
    [Register(nameof(CityListFragment))]
    public class CityListFragment : BaseFragment<CityListVM>, IScrollDirectorListener
    {
        #region Fields

        private MvxRecyclerView _recyclerView;
        private FloatingActionButton _fab;

        #endregion

        protected override int FragmentId => Resource.Layout.page_city_list;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ParentActivity.SupportActionBar.Title = Strings.city_list_title;
            ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ParentActivity.SupportActionBar.SetHomeButtonEnabled(true);

            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.city_list);
            if (_recyclerView != null)
            {
                var layoutManager = new LinearLayoutManager(Activity);
                _recyclerView.SetLayoutManager(layoutManager);
            }

            _fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_add_city);
            _fab.AttachToRecyclerView(_recyclerView, this);
            _fab.Enabled = true;

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_save_cities, menu);

            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_save_city:
                    ViewModel.SaveChangeCommand.Execute();
                    return true;
                case global::Android.Resource.Id.Home:
                    ViewModel.SaveChangeCommand.Execute();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void OnScrollDown()
        {
        }

        public void OnScrollUp()
        {
        }
    }
}