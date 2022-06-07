using System.ComponentModel.DataAnnotations;

namespace ProgettoFinale.Models.Communication
{
    public class SaveCityResource
    {
        // public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public City ToCity() => new City()
        {
            Name = this.Name,
            CountryId = this.CountryId,

        };
    }
}
