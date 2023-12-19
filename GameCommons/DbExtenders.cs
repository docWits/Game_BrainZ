using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfGameApp
{

    public static class DbExtenders
    {
        /// <summary>
        /// Добавление несуществующего объекта
        /// <para>Проверка осуществляется по наименованию <see cref="Entities.Entity.Name"/></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="entities"></param>
        public static void AddOrUpdateByName<T>(this DbSet<T> dbSet, params T[] entities) where T : Entities.Costable, new()
        {
            // Обработка всех объектов по порядку следования
            foreach (T entity in entities)
            {
                // Проверка на существование одноименного объекта
                T fromdb = dbSet.FirstOrDefault(x => x.Name == entity.Name);
                if (fromdb == null)
                {
                    // Добавление объекта
                    dbSet.Add(entity);
                }
                else 
                {
                    // Обновление объекта в БД
                    fromdb.Price = entity.Price;
                    fromdb.VendorID = entity.VendorID;
                }
            }
        }
    }
}
