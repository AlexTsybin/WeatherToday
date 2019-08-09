using Android.Support.V4.App;
using MvvmCross.Droid.Support.V4;

namespace WeatherToday.Android.Presenter
{
    public interface ICustomPresenter
    {
        bool CanPop(FragmentManager fragmentManager = null);

        void MoveBack(FragmentManager fragmentManager);

        MvxFragment GetTopFragment();
    }
}