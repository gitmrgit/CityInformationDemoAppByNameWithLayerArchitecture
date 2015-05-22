using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityInformationDemoAppByNameWithLayerArchitecture.DAL;
using CityInformationDemoAppByNameWithLayerArchitecture.Model;

namespace CityInformationDemoAppByNameWithLayerArchitecture.BLL
{
    class CityManager
    {
        CityGateway cityGateway = new CityGateway();


        public List<City> LoadAllCity() 
        {
            return cityGateway.LoadAllCity();
        }

        public int UpdateCityByName(City city) 
        {
            return cityGateway.UpdateCityByName(city);
        }

        public string SaveCity(City city) 
        {
            if (city.CityName.Length < 4)
            {
                return "City name must be 4 characters long";
            }
            else 
            {
                if (cityGateway.IsCityNameExist(city.CityName))
                {
                    return "City name already exists";
                }
                else 
                {
                    if (cityGateway.SaveCityInformation(city) < 1)
                    {
                        return "Not inserted .";
                    }
                    else 
                    {
                        return "Inserted.";
                    }
                }
            }
        }


        public List<City> SearchCityByCityName(string cityName)
        {
            return cityGateway.SearchCityByCityName(cityName);
        }

        public List<City> SearchCityByCountryName(string countryName)
        {
            return cityGateway.SearchCityByCountryName(countryName);
        }
             
    }

}
