namespace TestWebAPI_Project.Models.DTO
{
	public class AddRegionDto
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string? RegionImageUrl { get; set; }
        public IFormFile PdfFile { get; set; }
    }
}
