using System;
using System.Collections.Generic;

namespace AutoLotDAL.Repos
{
    interface IRepo<T>
    {
        Int32 Add(T entity);
        Int32 AddRange(IList<T> entities);
        Int32 Save(T entity);
        Int32 Delete(Int32 id, Byte[] timestamp);
        Int32 Delete(T entity);
        T GetOne(Int32? id);
        List<T> GetAll();
        List<T> ExecuteQuery(String sql);
        List<T> ExecuteQuery(String sql, Object[] sqlParametersObjects);
    }
}
