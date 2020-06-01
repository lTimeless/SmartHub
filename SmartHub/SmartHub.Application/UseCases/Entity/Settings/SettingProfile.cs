using AutoMapper;
using SmartHub.Domain.Entities.Settings;

namespace SmartHub.Application.UseCases.Entity.Settings
{
	public class SettingProfile : Profile
	{
		public SettingProfile()
		{
			// <Input, Output>
			CreateMap<Setting, SettingDto>()
				.PreserveReferences()
				.ReverseMap(); // bidirectional mapping


		}
	}
}
