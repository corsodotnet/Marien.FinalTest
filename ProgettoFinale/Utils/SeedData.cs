using Microsoft.EntityFrameworkCore;
using ProgettoFinale.Models;
using ProgettoFinale.Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgettoFinale.Utils
{
    public static class SeedData
    {
        public static async Task SeedDatabase(DatabaseCxt dbCtx)
        {
            Clear(dbCtx.City);
            Clear(dbCtx.Country);
            Clear(dbCtx.Continent);

            Random rand = new Random();

            Continent europe = new Continent()
            {
                Name = "Europe",
                nPositivi = 0,
                nAbitanti = 0,
                Area = "White",
                Countries = new List<Country>()
            };

            List<Country> countries = new List<Country>()
            {
                new Country() { Name = "Italia", nPositivi=0, Area="", Cities=new List<City>(), Continent=europe },
                new Country() { Name = "Francia", nPositivi=0, Area="", Cities=new List<City>(), Continent=europe},
                new Country() { Name = "Spagna", nPositivi=0, Area="", Cities=new List<City>(), Continent=europe }
            };
            List<City> citiesI = new List<City>()
            {
                new City() { Name = "Torino", nAbitanti=500000, nPositivi=50000, Area="", Country=countries[0]},
                new City() { Name = "Milano", nAbitanti=500000, nPositivi=500000, Area="", Country=countries[0]},
                new City() { Name = "Genova", nAbitanti=500000, nPositivi=50000, Area="", Country=countries[0]},
                new City() { Name = "Roma", nAbitanti=500000, nPositivi=50000, Area="", Country=countries[0]},
            };
            List<City> citiesF = new List<City>()
            {
                new City() { Name = "Paris", nAbitanti=500000, nPositivi=50000, Area="", Country=countries[1]},
                new City() { Name = "Nice", nAbitanti=500000, nPositivi=50000, Area="", Country=countries[1]},
                new City() { Name = "Menton", nAbitanti=500000, nPositivi=5000, Area="", Country=countries[1]},
            };
            List<City> citiesS = new List<City>()
            {
                new City() { Name = "Madrid", nAbitanti=500000, nPositivi=50000, Area="", Country=countries[2]},
                new City() { Name = "Barcelona", nAbitanti=500000, nPositivi=20000, Area="", Country=countries[2]},
                new City() { Name = "Valencia", nAbitanti=500000, nPositivi=10000, Area="", Country=countries[2]},
            };

            europe.Countries.AddRange(countries);

            countries[0].Cities.AddRange(citiesI);
            countries[0].nabitanti();
            var npositivic0 = countries[0].npositivi();
            countries[0].Area = AreaC(npositivic0);

            countries[1].Cities.AddRange(citiesF);
            countries[1].nabitanti();
            var npositivic1 = countries[1].npositivi();
            countries[1].Area = AreaC(npositivic1);

            countries[2].Cities.AddRange(citiesS);
            countries[2].nabitanti();
            var npositivic2 = countries[2].npositivi();
            countries[2].Area = AreaC(npositivic2);

            europe.nabitanti();
            europe.npositivi();

            dbCtx.Continent.Add(europe);

            try
            {
                await dbCtx.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static string AreaC(int number)
        {   
            //>10k
            if (number <= ((int)AreaZone.WHITE))
                return "White";
            //10k - 100k
            else if ((number >= ((int)AreaZone.WHITE)) && (number <= ((int)AreaZone.YELLOW)))
                return "Yellow";
            // >100k - 500k
            else if ((number >= ((int)AreaZone.YELLOW)) && (number <= ((int)AreaZone.ORANGE)))
                return "Orange";
            //>500k
            else if (number >= ((int)AreaZone.RED))
                return "Red";
            return "";
        }

        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            if (dbSet.Any())
            {
                dbSet.RemoveRange(dbSet.ToList());
            }
        }
    }
}
