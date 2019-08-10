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

        public Task<int> SaveCityAsync(CityBO note)
        {
            if (note.ID != 0)
            {
                return _database.UpdateAsync(note);
            }
            else
            {
                return _database.InsertAsync(note);
            }
        }

        public Task<int> DeleteCityAsync(CityBO note)
        {
            return _database.DeleteAsync(note);
        }
    }
}
