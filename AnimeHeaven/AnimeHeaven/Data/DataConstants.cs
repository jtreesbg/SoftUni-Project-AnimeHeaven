namespace AnimeHeaven.Data
{
    public class DataConstants
    {
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
            public const int NameMaxLength = 25;
            public const int PhoneNumberMaxLength = 30;
        }
    }
}
