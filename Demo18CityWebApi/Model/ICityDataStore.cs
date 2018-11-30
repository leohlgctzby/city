using System;
using System.Collections.Generic;

namespace Demo18CityWebApi.Model
{
    public interface ICityDataStore
    {
		IEnumerable<CityDto> GetAllCities();
		CityDto GetCityByID(int cityID);
		void AddCity(CityDto city);
		CityDto UpdateCity(int course, CityDto city);
		bool DeleteCity(int cityID);
		bool Save();
    }
}
