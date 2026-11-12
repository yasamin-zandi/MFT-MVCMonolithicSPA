using MonolithicSPA.ApplicationService.Dtos;

namespace MonolithicSPA.ApplicationService.Contracts
{
    public interface IPersonApplicationService
    {

        #region get-by-id
        Task<EditPersonDto?> GetbyId(int id);
        #endregion

        #region Update

        Task Update(EditPersonDto dto);
        #endregion

        #region Post
        Task PostPerson(PostPersonDto postPersonDto);

        #endregion

        #region Delete
        Task RemovePerson(int id);
        Task RemovePerson(EditPersonDto personDto);
        #endregion

        #region Get all
        Task<List<GetPersonDto>> GetAllPeople();
        #endregion
    }
}
