using PetShop.DeveloperTesting.DomainLayer.Context;
using PetShop.DeveloperTesting.DomainLayer.Models;

namespace RepositoryLayer
{   
    public interface IRepository<T> where T : BaseEntity
    {        
        IEnumerable<T> GetAll();       
        T? Get(int id);
                
        int Insert(T entity);
       
        void Update(T entity);

        void Delete(int entityId);

        //void SaveChanges();
    }
}
