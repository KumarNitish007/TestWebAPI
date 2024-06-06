namespace TestWebAPI_Project.Models.DTO
{
	public class RegionDto
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string? RegionImageUrl { get; set; }
        public IFormFile PdfFile { get; set; } // New property for the PDF file
        public string PdfFilePath { get; internal set; }
    }
}
