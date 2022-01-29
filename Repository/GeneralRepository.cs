using API2.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API2.Repository.Interface
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet <Entity> entities;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            var result = entities.Find(key);
            return result;
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Update(Entity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();  
            return result;
        }

        public int Delete(Key key)
        {
            var find = entities.Find(key);//MENCARI DATA
            if (find == null)
            {
                return 1;
            }
            else
            {
                entities.Remove(find);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

    }
}
