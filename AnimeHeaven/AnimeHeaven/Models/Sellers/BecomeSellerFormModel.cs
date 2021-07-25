﻿namespace AnimeHeaven.Models.Sellers
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Seller;
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
