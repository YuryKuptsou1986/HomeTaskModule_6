using BasketService.DAL.LiteDb.Providers;
using BasketService.Domain.Entities;
using LiteDB;

namespace BasketService.DAL.LiteDb.DbContext
{
    public class LiteDBContext : ILiteDBContext
    {
        public ILiteDatabase Database { get; }

        public LiteDBContext(ILiteDbSettingsProvider settingsProvider)
        {
            Database = new LiteDatabase(settingsProvider.ProvideSettings().DataBaseLocation);

            Database.Mapper.Entity<Cart>().DbRef(x => x.Items, nameof(Item));
            Database.Mapper.Entity<Item>().Id(x => x.Id, true);
            Database.GetCollection<Item>().EnsureIndex(x => x.Id);
        }
    }
}
