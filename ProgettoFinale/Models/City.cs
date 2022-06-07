using System.Collections.Generic;

namespace ProgettoFinale.Models
{
    public class City : AreaGeografica
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public City ToResource() => new City()
        {
            Name = this.Name,
            CountryId = this.CountryId,

        };
    }
}
