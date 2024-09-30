﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.Drawing;
using TheSouq.Core.Enities;
using static System.Net.Mime.MediaTypeNames;

namespace TheSouq.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly Cloudinary _cloudainry;

		private List<string> _allowedExtentions = new() { ".jpg", ".png", ".jpeg" };
		private int _maxAllowedSize = 2097152; // 2MB(inBytes) 2 * 1024 * 1024

		public ProductsController(IUnitOfWork unitOfWork, IMapper mapper,IOptions<CloudainrySettings> cloudainry)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;

			Account account = new()
			{
				Cloud = cloudainry.Value.Cloud,
				ApiKey = cloudainry.Value.ApiKey,
				ApiSecret = cloudainry.Value.ApiSecret,
			};

			_cloudainry = new Cloudinary(account);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var products = await _unitOfWork.Products.FindAllAsync(p => p.Id >= 1, new[] { "Size", "Category", "Color" });

			if (products is null)
				return NotFound();

			var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

			return Ok(productsDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var product = await _unitOfWork.Products.FindAsync(p => p.Id == id, new[] {"Size","Category","Color"});

			if (product is null)
				return NotFound();

			var productDto = _mapper.Map<ProductDto>(product);

			return Ok(productDto);
		}

		[HttpPost]
		public async Task<IActionResult> AddProductAsync([FromForm]ProductFormDto Dto)
		{

			
			var product = _mapper.Map<Product>(Dto);

			if (Dto.Image is not null)
			{
				var extention = Path.GetExtension(Dto.Image.FileName);

				if (Dto.Image.Length > _maxAllowedSize)
					return BadRequest("Image sholud be less than or equal 2 MB");

				if (!_allowedExtentions.Contains(extention))
					return BadRequest("allowed extentsions are : \".jpg\", \".png\", \".jpeg\" ");

				var ImageName = $"{Guid.NewGuid()}{extention}";

				using var stream = Dto.Image.OpenReadStream();

				var imageParams = new ImageUploadParams
				{
					File = new FileDescription(ImageName, stream)
				};

				var result = await _cloudainry.UploadAsync(imageParams);

				product.ImageUrl = result.SecureUrl.ToString();
			}

			product.IsDeleted = false;
			product.CreatedAt = DateTime.UtcNow;
			

			await _unitOfWork.Products.AddAsync(product);
			_unitOfWork.Commit();

			//var link = Url.Link(nameof(GetByIdAsync), new { id = product.Id });
			var productDto = _mapper.Map<ProductDto>(product);


			return Ok(productDto);

		}


		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var product = _unitOfWork.Products.GetById(id);

			if (product is null)
				return NotFound();

			var productDto = _mapper.Map<ProductDto>(product);

			_unitOfWork.Products.Delete(product);
			_unitOfWork.Commit();

			return Ok(productDto);
		}



		


	}
}