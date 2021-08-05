namespace AnimeHeaven.Models.Sellers
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Seller;
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(AddressMaxLength)]
        public string Address { get; set; }
    }
}
