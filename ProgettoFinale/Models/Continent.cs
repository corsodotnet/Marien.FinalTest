using ProgettoFinale.Contracts;
using System.Collections.Generic;

namespace ProgettoFinale.Models
{
    public class Continent : AreaGeografica
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public new int nAbitanti { get; set; }
        public new int nPositivi { get; set; }

        public new string Area { get; set; }

        public virtual List<Country> Countries { get; set; }

        public int nabitanti()
        {
            foreach (var city in Countries)
            {
                nAbitanti += city.nAbitanti;
            }
            return nAbitanti;
        }

        public int npositivi()
        {
            foreach (var city in Countries)
            {
                nPositivi += city.nPositivi;
            }
            return nPositivi;
        }

    }
}
