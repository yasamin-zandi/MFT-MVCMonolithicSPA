using Microsoft.EntityFrameworkCore;
using MonolithicSPA.Models.DomainModels.PersonAggregate;
using MonolithicSPA.Models.Services.Contracts;

namespace MonolithicSPA.Models.Services.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ProjectDbContext _projectDbContext;

        #region ctor
        public PersonRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;

        }
        #endregion


        #region Insert
        public async Task Insert(Person person)
        {
            try
            {
                _projectDbContext.Person.Add(person);
                await _projectDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update
        public async Task Update(Person person)
        {

            _projectDbContext.Person.Update(person);
            await _projectDbContext.SaveChangesAsync();

        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {

            var person = await _projectDbContext.Person.FindAsync(id);
            if (person != null)
            {
                _projectDbContext.Person.Remove(person);
                await _projectDbContext.SaveChangesAsync();
            }

        }
        public async Task Delete(Person person)
        {
            if (person != null)
            {
                _projectDbContext.Remove(person);
                await _projectDbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Select-By-Id
        public async Task<Person?> SelectbyId(int id)
        {
            return await _projectDbContext.Person.FindAsync(id);
        }
        #endregion

        #region SelectAll
        public async Task<List<Person>> SelectAll()
        {
            try
            {
                return await _projectDbContext.Person.ToListAsync();


            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion
    }
}
