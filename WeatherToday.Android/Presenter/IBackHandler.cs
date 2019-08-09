using System.Threading.Tasks;

namespace WeatherToday.Android.Presenter
{
    public interface IBackHandler
    {
        Task<bool> BackPressed();
    }
}