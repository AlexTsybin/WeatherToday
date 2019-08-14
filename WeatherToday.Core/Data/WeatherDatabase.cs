using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherToday.Core.Models.City;

namespace WeatherToday.Core.Data
{
    public class WeatherDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public WeatherDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<CityBO>().Wait();
        }

        public Task<List<CityBO>> GetCitiesAsync()
        {
            return _database.Table<CityBO>().ToListAsync();
        }

        public Task<CityBO> GetCityAsync(int id)
        {
            return _database.Table<CityBO>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<CityBO> GetCityByNameAsync(string name)
        {
            return _database.Table<CityBO>()
                            .Where(i => i.CityName == name)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveCityAsync(CityBO city)
        {
            if (city.ID != 0)
            {
                return _database.UpdateAsync(city);
            }
            else
            {
                return _database.InsertAsync(city);
            }
        }

        public Task<int> DeleteCityAsync(CityBO city)
        {
            return _database.DeleteAsync(city);
        }
    }
}
