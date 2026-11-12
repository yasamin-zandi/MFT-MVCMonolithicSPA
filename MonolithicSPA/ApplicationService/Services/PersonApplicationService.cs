using MonolithicSPA.ApplicationService.Contracts;
using MonolithicSPA.ApplicationService.Dtos;
using MonolithicSPA.Models.DomainModels.PersonAggregate;
using MonolithicSPA.Models.Services.Contracts;

namespace MonolithicSPA.ApplicationService.Services
{
    public class PersonApplicationService : IPersonApplicationService
    {
        private readonly IPersonRepository _personRepositories;

        #region Ctor
        public PersonApplicationService(IPersonRepository personRepositories)
        {

            _personRepositories = personRepositories;
        }
        #endregion

        #region Post
        public async Task PostPerson(PostPersonDto postPersonDto)
        {
            var person = new Person()
            {
                FirstName = postPersonDto.FirstName,
                LastName = postPersonDto.LastName,
                NationalId = postPersonDto.NationalId
            };
            await _personRepositories.Insert(person);
        }
        #endregion

        #region Edit
        public async Task<EditPersonDto?> GetbyId(int id)
        {
            var person = await _personRepositories.SelectbyId(id);
            if (person == null) return null;
            return new EditPersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                NationalId = person.NationalId
            };
        }

        public async Task Update(EditPersonDto dto)
        {
            var person = await _personRepositories.SelectbyId(dto.Id);
            if (person == null)
                throw new KeyNotFoundException("شخص پیدا نشد!");

            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.NationalId = dto.NationalId;

            await _personRepositories.Update(person);
        }


        #endregion

        #region Remove
        public async Task RemovePerson(int id)
        {
            try
            {
                await _personRepositories.Delete(id);

            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task RemovePerson(EditPersonDto personDto)
        {
            try
            {
                var person = new Person
                {
                    Id = personDto.Id,
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    NationalId = personDto.NationalId
                };

                await _personRepositories.Delete(person);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region GetAll

        public async Task<List<GetPersonDto>> GetAllPeople()
        {
            try
            {


                var people = await _personRepositories.SelectAll();
                var PersonDtos = new List<GetPersonDto>();
                foreach (var person in people)
                {
                    var getPersonDto = new GetPersonDto()
                    {
                        NationalId = person.NationalId,
                        Id = person.Id,
                        LastName = person.LastName,
                        FirstName = person.FirstName,
                    };
                    PersonDtos.Add(getPersonDto);
                }
                return PersonDtos;
            }
            catch
            {

                throw;
            }
        }




        #endregion
    }
}
