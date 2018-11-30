using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Demo18CityWebApi.Model;

namespace Demo18CityWebApi.Controllers
{
    [Route("api/city")]
    public class CityController : Controller
    {
        
		private ICityDataStore _dataStore;

		public CityController(ICityDataStore dataStore){
			_dataStore = dataStore;
		}

        [HttpGet]
        public IActionResult Get()
        {
			var result = _dataStore.GetAllCities();
			return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
			var cityResult = _dataStore.GetCityByID(id);
            IActionResult result;
            if(cityResult == null) {
                result = NotFound();
            } else {
                result = Ok(cityResult);
            }
            return result;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CityDto value)
        {
			_dataStore.AddCity(value);
            return Ok(value);
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        [HttpPut]
        public IActionResult Put(int id, [FromBody]CityDto value)
        {
            //TODO:Check value value if it valid
			CityDto resultCity = _dataStore.UpdateCity(id,value);
            IActionResult result;
            if(resultCity != null) {
                result = Accepted(resultCity);
            } else {
                result = NotFound();
            }
            return result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult result;
			bool isRecordExist = _dataStore.DeleteCity(id);
            if(isRecordExist) {
                result = NoContent(); 
            }else {
                result = NotFound();
            }
            return result;
        }
    }
}
