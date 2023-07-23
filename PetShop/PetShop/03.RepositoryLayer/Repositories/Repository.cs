using Microsoft.EntityFrameworkCore;
using PetShop.DeveloperTesting.DomainLayer.Context;
using PetShop.DeveloperTesting.DomainLayer.Models;

namespace RepositoryLayer
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {        
        private readonly PetsDbContext _petsDbContext;
     
        private DbSet<T> entityDbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationDbContext">Contextul aplicatiei</param>
        public Repository(PetsDbContext applicationDbContext)
        {
            _petsDbContext = applicationDbContext;            
            entityDbSet = _petsDbContext.Set<T>();
        }   
        

        public IEnumerable<T> GetAll()
        {
            return entityDbSet.AsEnumerable();
        }
        public T? Get(int id)
        {            
            return entityDbSet.SingleOrDefault(c => c.Id == id);
        }
   
        public int Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entityDbSet.Add(entity);
            _petsDbContext.SaveChanges();

            return entity.Id;
        }

        public void Delete(int entityId)
        {
            var entity = entityDbSet.SingleOrDefault(c => c.Id == entityId);
            if (entity == null) return;

            entityDbSet.Remove(entity);
            _petsDbContext.SaveChanges();
        }
      
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entityDbSet.Update(entity);
            _petsDbContext.SaveChanges();
        }  

        //public void SaveChanges()
        //{
        //    _petsDbContext.SaveChanges();
        //}
    }
}
