using System.Collections.Generic;

namespace MinesweeperWithSolver.Data.Services.DataService
{
    public interface IDataService<T>
    {
        List<T> GetAll();
        T Get(int id);
        bool Create(T entity);
        bool Delete(int id);
        bool Update(int id, T entity);
    }
}
