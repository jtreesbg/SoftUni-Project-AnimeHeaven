namespace AnimeHeaven.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Seller;
    public class Seller
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}