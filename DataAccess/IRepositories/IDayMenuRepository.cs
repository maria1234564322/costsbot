using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IDayMenuRepository
    {
        void Add(DayMenu dayMenu);
        void Update(DayMenu dayMenu);
        void Remove(DayMenu dayMenu);
        DayMenu? GetById(int id);
        List<DayMenu> GetAll();
        void SaveChanges();
    }
}
