using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using WeatherToday.Core.ViewModels.Base;
using WeatherToday.Core.ViewModels.Weather;

namespace WeatherToday.Android.Views
{
    [MvxFragmentPresentation(typeof(WeatherViewModel), Resource.Id.content_frame, false,
        ViewModelType = typeof(WeatherListVM))]
    [Register(nameof(WeatherFragment))]
    public class WeatherFragment : BaseFragment<WeatherListVM>
    {
        protected override int FragmentId => Resource.Layout.page_weather_list;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.chat_users_list);
            if (recyclerView != null)
            {
                var layoutManager = new LinearLayoutManager(Activity);
                recyclerView.SetLayoutManager(layoutManager);
            }

            return view;
        }
    }
}