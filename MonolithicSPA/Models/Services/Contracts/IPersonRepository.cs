using MonolithicSPA.Models.DomainModels.PersonAggregate;

namespace MonolithicSPA.Models.Services.Contracts
{
    public interface IPersonRepository
    {

        #region Insert
        Task Insert(Person person);
        #endregion

        #region Select-By-Id
        Task<Person?> SelectbyId(int id);
        #endregion

        #region Update
        Task Update(Person person);
        #endregion

        #region Delete
        Task Delete(int id);
        Task Delete(Person person);
        #endregion

        #region selectAll
        Task<List<Person>> SelectAll();
        #endregion
    }
}
