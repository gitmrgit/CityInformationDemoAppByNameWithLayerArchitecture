using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityInformationDemoAppByNameWithLayerArchitecture.Model;
using System.Data.SqlClient;
using System.Configuration;

namespace CityInformationDemoAppByNameWithLayerArchitecture.DAL
{
    class CityGateway
    {
        string connectionStr = ConfigurationManager.ConnectionStrings[1].ConnectionString;
        public List<City> LoadAllCity() 
        {
            List<City> cityList = new List<City>();
            using (SqlConnection con = new SqlConnection(connectionStr)) 
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCity",con);
                con.Open();
                SqlDataReader rdr =  cmd.ExecuteReader();

                while(rdr.Read())
                {
                    City city = new City();
                    city.CityName = rdr[1].ToString();
                    city.About = rdr[2].ToString();
                    city.Country = rdr[3].ToString();

                    cityList.Add(city);
                }
            }
            return cityList;
        }

        public int UpdateCityByName(City city) 
        {
            int rowsUpdated = 0;
            using (SqlConnection con = new SqlConnection(connectionStr)) 
            {
                SqlCommand cmd = new SqlCommand("Update tblCity set About='"+city.About+"',Country='"+city.Country+"' where Name='"+city.CityName+"'",con);
                con.Open();
               rowsUpdated =  cmd.ExecuteNonQuery();
            }
            return rowsUpdated;
        }

        public bool IsCityNameExist(string cityName) 
        {
            bool isCityExist = false;
            using (SqlConnection con = new SqlConnection(connectionStr)) 
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCity Where Name='"+cityName+"'",con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) 
                {
                    isCityExist = true;
                }
            }
            return isCityExist;
        }

        public int SaveCityInformation(City city) 
        {
            int rowsInserted = 0;
            using (SqlConnection con = new SqlConnection(connectionStr)) 
            {
                SqlCommand cmd = new SqlCommand("insert into tblCity values('"+city.CityName+"','"+city.About+"','"+city.Country+"')",con);
                con.Open();
                rowsInserted  = cmd.ExecuteNonQuery();


            }

            return rowsInserted;
        }

        public List<City> SearchCityByCityName(string cityName)
        {
            List<City> cityList = new List<City>();
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCity where Name like '%" + cityName + "%'", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    City city = new City();
                    
                    city.CityName = rdr[1].ToString();
                    city.About = rdr[2].ToString();
                    city.Country = rdr[3].ToString();

                    cityList.Add(city);
                }
            }
            return cityList;
        }

        public List<City> SearchCityByCountryName(string countryName)
        {
            List<City> cityList = new List<City>();
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCity where Country like '%" + countryName + "%'", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    City city = new City();
                  //  city.cityID = int.Parse(rdr[0].ToString());
                    city.CityName = rdr[1].ToString();
                    city.About = rdr[2].ToString();
                    city.Country = rdr[3].ToString();

                    cityList.Add(city);
                }
            }
            return cityList;
        }
    }
}
