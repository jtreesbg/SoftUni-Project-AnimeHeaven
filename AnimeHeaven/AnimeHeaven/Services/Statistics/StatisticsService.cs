namespace AnimeHeaven.Services.Statistics
{
    using System.Linq;
    using AnimeHeaven.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly AnimeHeavenDbContext data;

        public StatisticsService(AnimeHeavenDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel Total()
        {
            var totalProducts = this.data.Products.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalProducts = totalProducts,
                TotalUsers = totalUsers
            };

        }
    }
}
