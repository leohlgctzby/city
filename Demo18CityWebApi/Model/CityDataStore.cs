using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Demo18CityWebApi.Model
{
	public class CityDataStore:ICityDataStore
    {
		private CityDBContext _ctx;

		public CityDataStore(CityDBContext ctx) {
			_ctx = ctx;
		}
              
		IEnumerable<CityDto> ICityDataStore.GetAllCities()
		{
			return _ctx.Cities.ToList();
		}

		public CityDto GetCityByID(int cityID)
        {
			return _ctx.Cities.Find(cityID);
        }

        public void AddCity(CityDto city)
        {
			_ctx.Cities.Add(city);
			Save();
        }

		public CityDto UpdateCity(int cityID, CityDto city)
		{
			CityDto oddCity = _ctx.Cities.Find(cityID);
			oddCity.Name = city.Name;
			oddCity.Description = city.Description;
			Save();
			return _ctx.Cities.Find(cityID);
		}
        
		bool ICityDataStore.DeleteCity(int cityID)
		{
			CityDto oddCity = _ctx.Cities.Find(cityID);
			if(oddCity != null){
				_ctx.Cities.Remove(oddCity);
			}
			return Save();

		}

		public bool Save()
		{
			return (_ctx.SaveChanges() >= 0);
		}
	}
}
