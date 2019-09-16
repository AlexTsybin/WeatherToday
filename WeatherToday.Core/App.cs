using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;
using System.IO;
using WeatherToday.Core.Data;
using WeatherToday.Core.ViewModels.Base;

namespace WeatherToday.Core
{
    public class App : MvxApplication
    {
        static WeatherDatabase database;

        public override void Initialize()
        {
            new API.App().Initialize();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterCustomAppStart<WeatherAppStart<MainVM>>();
        }

        public static WeatherDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new WeatherDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Weather.db3"));
                }
                return database;
            }
        }
    }
}
