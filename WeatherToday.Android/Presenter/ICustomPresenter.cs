using Android.Support.V4.App;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Views;

namespace WeatherToday.Android.Presenter
{
    public interface ICustomPresenter
    {
        bool CanPop(FragmentManager fragmentManager = null);

        void MoveBack(FragmentManager fragmentManager);

        MvxFragment GetTopFragment();

        void DeleteCurrentFragmentFromActivity(FragmentManager fragmentManager, IMvxFragmentView fragment);
    }
}