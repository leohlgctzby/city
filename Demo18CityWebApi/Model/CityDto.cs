using System;
namespace Demo18CityWebApi.Model
{
    public class CityDto
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int NumberOfPointInterest { get; set; }

        public CityDto()
        {
        }
    }
}
