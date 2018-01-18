using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;

namespace MEUpload
{
    public class DB_Fun
    {
        //public static ISession session = MyHibernateHelper.SessionFactory.OpenSession();

        public static int Delete<TEntity>(ISession session, object id)
        {
            var queryString = string.Format("delete {0} where id = :id",
                                            typeof(TEntity));
            return session.CreateQuery(queryString)
                          .SetParameter("id", id)
                          .ExecuteUpdate();
        }

    }
}
