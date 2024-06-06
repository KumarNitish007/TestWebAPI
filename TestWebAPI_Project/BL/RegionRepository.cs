using Microsoft.EntityFrameworkCore;
using TestWebAPI_Project.Data;
using TestWebAPI_Project.Models.Domain;

namespace TestWebAPI_Project.BL
{
	public class RegionRepository : IRegionRepository
	{
		private readonly TestDbContext dbContext;

		public RegionRepository(TestDbContext dbContext)
        {
			this.dbContext = dbContext;
		}

		public async Task<Region> CreateAsync(Region region)
		{
			await dbContext.AddAsync(region);
			await dbContext.SaveChangesAsync();
			return region;
		}

		public async Task<Region?> DeleteAsync(Guid id)
		{
			var existingregion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
			if (existingregion==null)
			{
				return null;
			}
		    dbContext.Regions.Remove(existingregion);
			await dbContext.SaveChangesAsync();
			return existingregion;
		}

		public async Task<List<Region>> GetAllAsync()
		{
			return await dbContext.Regions.ToListAsync();
		}

		public async Task<Region?> GetByIdAsync(Guid Id)
		{
			return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
		}

		public async Task<Region?> UpdateAsync(Guid id, Region region)
		{
			var existinfregion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

			if (existinfregion==null)
			{
				return null;
			}
			existinfregion.Code = region.Code;
			existinfregion.Name = region.Name;
			existinfregion.RegionImageUrl = region.RegionImageUrl;
			await dbContext.SaveChangesAsync();
			return existinfregion;
		}
	}
}
