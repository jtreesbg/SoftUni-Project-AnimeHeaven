namespace AnimeHeaven.Test.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using AnimeHeaven.Data;

    public static class DataBaseMock
    {
        public static AnimeHeavenDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<AnimeHeavenDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

                return new AnimeHeavenDbContext(dbContextOptions);
            }
        }
    }
}
