using MinesweeperWithSolver.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesweeperWithSolver.Data.Services.DataService
{
    public class GenericDataService<T> : IDataService<T> where T : BaseTable
    {
        private readonly DatabaseContext _dbContext;

        public GenericDataService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                T entity = _dbContext.Set<T>().FirstOrDefault((e) => e.Id == id);
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T Get(int id)
        {
            T entity = _dbContext.Set<T>().FirstOrDefault((e) => e.Id == id);
            return entity;
        }

        public List<T> GetAll()
        {
            List<T> entites = _dbContext.Set<T>().ToList();
            return entites;
        }

        public bool Update(int id, T entity)
        {
            try
            {
                T currentEntity = _dbContext.Set<T>().FirstOrDefault((e) => e.Id == id);
                entity.Id = currentEntity.Id;
                _dbContext.Entry(currentEntity).CurrentValues.SetValues(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
