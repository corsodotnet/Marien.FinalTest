using System.Collections.Generic;

namespace ProgettoFinale.Models
{
    public class Country : AreaGeografica
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public new int nAbitanti { get; set; }
        public new int nPositivi { get; set; }

        public new string Area { get; set; }

        public virtual Continent Continent { get; set; }

        public virtual List<City> Cities { get; set; }

        public int nabitanti()
        {
            foreach (var city in Cities)
            {
                nAbitanti += city.nAbitanti;
            }
            return nAbitanti;
        }

        public int npositivi()
        {
            foreach (var city in Cities)
            {
                nPositivi += city.nPositivi;
            }
            return nPositivi;
        }

    }
}
