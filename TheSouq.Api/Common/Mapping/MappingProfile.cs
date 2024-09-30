using TheSouq.Api.Common.DTOS;
using TheSouq.Core.Enities;

namespace TheSouq.Api.Common.Mapping
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			// Products
			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color!.Name))
				.ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size!.Name))
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name));

			CreateMap<ProductFormDto, Product>();
			
		}
	}
}
