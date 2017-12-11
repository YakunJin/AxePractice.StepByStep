using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;

namespace Orm.Practice
{
    public class AddressRepositoryQueryOverImpl : RepositoryBase, IAddressRepository
    {
        public AddressRepositoryQueryOverImpl(ISession session) : base(session)
        {
        }

        public Address Get(int id)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.Id == id).SingleOrDefault();

            #endregion
        }

        public IList<Address> Get(IEnumerable<int> ids)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().WhereRestrictionOn(a=>a.Id).IsIn(ids.ToArray()).OrderBy(a=>a.Id).Asc.List();

            #endregion
        }

        public IList<Address> GetByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().OrderBy(a=>a.Id).Asc.Where(a => a.City == city).List();

            #endregion
        }

        public Task<IList<Address>> GetByCityAsync(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().OrderBy(a=>a.Id).Asc.Where(a => a.City == city).ListAsync();

            #endregion
        }

        public Task<IList<Address>> GetByCityAsync(string city, CancellationToken cancellationToken)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.City == city).ListAsync(cancellationToken);

            #endregion
        }

        public IList<KeyValuePair<int, string>> GetOnlyTheIdAndTheAddressLineByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                 .Where(item => item.City == city)
                 .OrderBy(item => item.Id).Asc
                 .List()
                 .Select(a => new KeyValuePair<int, string>(a.Id, a.AddressLine1))
                 .ToList();

            #endregion
        }

        public IList<string> GetPostalCodesByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.City == city).Select(a => a.PostalCode).List<string>().Distinct().ToList();

            #endregion
        }
    }
}