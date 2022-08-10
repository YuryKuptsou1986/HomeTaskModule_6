using LiteDB;

namespace BasketService.DAL.LiteDb.DbContext
{
    public interface ILiteDBContext
    {
        ILiteDatabase Database { get; }
    }
}
