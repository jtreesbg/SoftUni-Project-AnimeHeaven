namespace AnimeHeaven.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Customer;

    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Favourites> Favourites { get; set; }
    }
}
