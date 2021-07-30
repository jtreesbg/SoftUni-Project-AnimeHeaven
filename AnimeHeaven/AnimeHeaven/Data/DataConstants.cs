namespace AnimeHeaven.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int FullNameMinLength = 2;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 4;
            public const int PasswordMaxLength = 100;
        }

        public class Products
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 27;
            public const int AnimeOriginMinLength = 0;
            public const int AnimeOriginMaxLength = 49;
            public const int DescriptionMinLength = 0;
            public const int DescriptionMaxLength = 300;
            public const int YearMinValue = 1990;
            public const int YearMaxValue = 2021;
            public const int ImageUrlMaxLength = 2048;
        }

        public class Category
        {
            public const int NameMaxLength = 25;
        }

        public class Seller
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }

        public class Customer   
        {
            public const int UsernameMinLength = 2;
            public const int UsernameMaxLength = 25;   
        }
    }
}
