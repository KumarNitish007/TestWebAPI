using TestWebAPI_Project.Models.Domain;

namespace TestWebAPI_Project.BL
{
	public interface IRegionRepository
	{
		Task<List<Region>> GetAllAsync();
		Task<Region?> GetByIdAsync(Guid Id);
		Task<Region> CreateAsync(Region region);
		Task<Region?> UpdateAsync(Guid id,Region region);
		Task<Region?> DeleteAsync(Guid id);
	}
}
