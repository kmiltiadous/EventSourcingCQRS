using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;

namespace EventSourcingCQRS.ReadModel.Persistence
{
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
