using Android.Content;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using static Android.Support.Design.Widget.Snackbar;

namespace WeatherToday.Android.Behaviour
{
    public class RefractoredFabBehavior : CoordinatorLayout.Behavior
    {
        public RefractoredFabBehavior(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            return dependency is SnackbarLayout;
        }

        // Используется для автоматического поднятия FAB при появлении Snackbar
        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            float translationY = Java.Lang.Math.Min(0, dependency.TranslationY - dependency.Height);

            (child as Refractored.Fab.FloatingActionButton).TranslationY = translationY;

            return true;
        }
    }
}