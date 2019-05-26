using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Model.Repository
{
    public interface IRepository<T>
    {
        int Insert(T entity);
        void Delete(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
        List<T> GetListT();
        bool CheckExist(int id);
        int CheckHasRows();
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        public static string operate = string.Empty;
        protected DbSet<VienChuc> _DbSetVienChuc;
        protected DbSet<T> _DbSet;
        protected QLNSSGU_1Entities _db;

        public Repository(QLNSSGU_1Entities db)
        {
            _DbSetVienChuc = db.Set<VienChuc>();
            _DbSet = db.Set<T>();
            _db = db;
        }

        #region IRepository<T> Members

        public int Insert(T entity)
        {
            _DbSet.Add(entity);
            operate = "insert";
            return 1;
        }

        public void Delete(T entity)
        {
            _DbSet.Remove(entity);
            operate = "delete";
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _DbSet;
        }

        public T GetById(int id)
        {
            return _DbSet.Find(id);
        }
        
        public List<T> GetListT()
        {
            return _DbSet.ToList();
        }

        public bool CheckExist(int id)
        {
            if (GetById(id) != null)
                return true;
            return false;
        }
        /// <summary>
        /// use any() instead count() to check if there is at least one element in the list, but will not enumerate the entire sequence.
        /// </summary>
        /// <returns></returns>
        public int CheckHasRows()
        {
            if (_DbSet.Any())
                return 1;
            return 0;
        }

        //private static Dictionary<string, string> buildLog(string actor = null, string message = "")
        //{
        //    Dictionary<string, string> re = new Dictionary<string, string>();
        //    try
        //    {
        //        if (actor != null)
        //        {
        //            re.Add("actor", actor);
        //        }
        //        else
        //        {
        //            re.Add("actor", "unknown");
        //        }

        //        if (operate != string.Empty)
        //        {
        //            re.Add("action", operate);
        //        }
        //        else
        //        {
        //            re.Add("action", "unknown");
        //        }
        //        re.Add("message", message);
        //        return re;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Debug.WriteLine(ex);
        //        re.Add("State: ", "ERROR");
        //        return re;
        //    }
        //}
        ///// <summary>
        ///// Auto commit
        ///// </summary>
        ///// <param name="message"></param>
        //public static void write(String message = "")
        //{
        //    try
        //    {
        //        LogHeThong tmp = new LogHeThong();
        //        tmp.onBeforeAdded();
        //        tmp.mota = StringHelper.toJSON(buildLog("execute", message));
        //        tmp.add();
        //        DBInstance.commit();
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e);
        //    }
        //}
        #endregion
    }
}
