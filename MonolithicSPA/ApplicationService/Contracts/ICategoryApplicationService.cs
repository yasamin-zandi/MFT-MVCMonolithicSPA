using MonolithicSPA.ApplicationService.Dtos;

namespace MonolithicSPA.ApplicationService.Contracts
{
    public interface ICategoryApplicationService
    {

        #region Select all category
        Task<List<GetCategoryDto>> GetCategoriesSelectList();
        #endregion
    }
}
