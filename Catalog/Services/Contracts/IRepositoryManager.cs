using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }

        Task Save();
    }
}
