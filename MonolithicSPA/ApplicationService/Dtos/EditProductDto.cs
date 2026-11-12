namespace MonolithicSPA.ApplicationService.Dtos
{
    public class EditProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryTitle { get; set; }
    }
}
