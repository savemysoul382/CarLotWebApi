using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.EF;
using AutoLotDAL.Models.Base;

namespace AutoLotDAL.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly AutoLotEntities _db;

        protected AutoLotEntities Context => this._db;

        public BaseRepo()
        {
            this._db = new AutoLotEntities();
            this._table = this._db.Set<T>();
        }

        internal int SaveChanges()
        {
            try
            {
                return this._db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Генерируется, когда возникает ошибка, связанная с параллелизмом.
                // Пока что просто повторно сгенерировать исключение,
                throw;
            }
            catch (DbUpdateException ex)
            {
                // Генерируется, когда обновление базы данных терпит неудачу.
                // Проверить внутреннее исключение (исключения), чтобы получить
                // дополнительные сведения и выяснить, на какие объекты это повлияло.
                // Пока что просто повторно сгенерировать исключение.
                throw;
            }
            catch (CommitFailedException ex)
            {
                // Обработать здесь отказы транзакции.
                // Пока что просто повторно сгенерировать исключение,
                throw;
            }
            catch (Exception ex)
            {
                // Произошло какое-то другое исключение, которое должно быть обработано,
                throw;
            }
        }


        public Int32 Add(T entity)
        {
            this._table.Add(entity);
            return SaveChanges();
        }

        public Int32 AddRange(IList<T> entities)
        {
            this._table.AddRange(entities);
            return SaveChanges();
        }

        public Int32 Save(T entity)
        {
            this._db.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        public Int32 Delete(Int32 id, Byte[] timestamp)
        {
            this._db.Entry(new T() {Id = id, Timestamp = timestamp}).State = EntityState.Deleted;
            return SaveChanges();
        }

        public Int32 Delete(T entity)
        {
            this._db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public T GetOne(Int32? id) => this._table.Find(id);

        public List<T> GetAll() => this._table.ToList();

        public List<T> ExecuteQuery(String sql) => this._table.SqlQuery(sql).ToList();

        public List<T> ExecuteQuery(String sql, Object[] sqlParametersObjects) => this._table.SqlQuery(sql, sqlParametersObjects).ToList();

        public void Dispose()
        {
            this._db?.Dispose();
        }
    }
}