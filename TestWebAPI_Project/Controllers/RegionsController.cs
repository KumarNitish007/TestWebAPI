using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPI_Project.BL;
using TestWebAPI_Project.Data;
using TestWebAPI_Project.Models.Domain;
using TestWebAPI_Project.Models.DTO;

namespace TestWebAPI_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly TestDbContext dbContext;
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public RegionsController(TestDbContext dbContext,IRegionRepository regionRepository,IMapper mapper, IConfiguration configuration)
        {
			this.dbContext = dbContext;
			this.regionRepository = regionRepository;
			this.mapper = mapper;
            this.configuration = configuration;
        }
        //Get All Region
        [HttpGet]
        public async Task<IActionResult> GetAllResults()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            var regionDtoList = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDtoList.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                    PdfFilePath = regionDomain.PdfFilePath 
                });
            }

            return Ok(regionDtoList);
        }

     

        //Get Singal region
        [HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id) 
		{
            //var resionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id); //We can do this through this process also
            var resionDomain = await regionRepository.GetByIdAsync(id);
            //var region = dbContext.Regions.Find(id); //  We can do this  process also
            if (resionDomain == null)
			{
				return NotFound();
			}
			var regiondto = new RegionDto
			{
				Id = resionDomain.Id,
				Code = resionDomain.Code,
				Name = resionDomain.Name,
				RegionImageUrl = resionDomain.RegionImageUrl,
			};
			  
			return Ok(regiondto);
		}

        //Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddRegionDto addRegionDto)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionDto.Code,
                Name = addRegionDto.Name,
                RegionImageUrl = addRegionDto.RegionImageUrl,
            };

            if (addRegionDto.PdfFile != null && addRegionDto.PdfFile.Length > 0)
            {
                var pdfUploadFolderPath = configuration["PdfUploadFolderPath"];
                var filePath = Path.Combine(pdfUploadFolderPath, $"{Guid.NewGuid()}_{Path.GetFileName(addRegionDto.PdfFile.FileName)}");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await addRegionDto.PdfFile.CopyToAsync(stream);
                }

                regionDomainModel.PdfFilePath = filePath;
            }

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
                PdfFilePath = regionDomainModel.PdfFilePath
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }
        [HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateregionRequestDto updateregionRequestDto)
		{

			//  Map DTO to Domain Model
			var regionDomainModel = new Region
			{
				Code = updateregionRequestDto.Code,
				Name = updateregionRequestDto.Name,
				RegionImageUrl = updateregionRequestDto.RegionImageUrl,
			};
			//var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

			regionDomainModel= await regionRepository.UpdateAsync(id, regionDomainModel);
			if (regionDomainModel==null)
			{
				return NotFound();
			}

			////Map DTO to Domain Model
			//regionDomainModel.Code = updateregionRequestDto.Code;
			//regionDomainModel.Name = updateregionRequestDto.Name;
			//regionDomainModel.RegionImageUrl = updateregionRequestDto.RegionImageUrl;
             //await dbContext.SaveChangesAsync();

			//convert Domain model to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl,
			};

			return Ok(regionDto);
		}
	
		//Delete
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			//var regionDomainModel= await dbContext.Regions.FirstOrDefaultAsync(s => s.Id == id); // this is different way
			var regionDomainModel = await regionRepository.DeleteAsync(id);
			if (regionDomainModel==null)
			{
				return NotFound();
			}

			//Return Delete Region Back
			//Map Domain Model to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl,
			};

			return Ok(regionDto);
		}
	}
}
