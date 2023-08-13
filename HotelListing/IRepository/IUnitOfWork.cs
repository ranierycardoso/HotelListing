using HotelListing.Data;

namespace HotelListing.IRepository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        Task Save();
        void Dispose();
    }
}
