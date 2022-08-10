using BasketService.DAL.LiteDb.Entities;

namespace BasketService.DAL.LiteDb.Providers
{
    public interface ILiteDbSettingsProvider
    {
        public LiteDbSettings ProvideSettings();
    }
}
