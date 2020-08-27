using MediatR;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	// TODO: is it now already so big that is should be called "InitCommand"???? probably... With own inithandler etc
	public class HomeCreateCommand : IRequest
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool AutoDetectAddress { get; set; }
		public bool AcceptStillWip { get; set; }
		public bool UseFakeDb { get; set; }

		public HomeCreateCommand(string name, string description, bool autoDetectAddress, bool acceptStillWip, bool useFakeDb)
		{
			Name = name;
			Description = description;
			AutoDetectAddress = autoDetectAddress;
			AcceptStillWip = acceptStillWip;
			UseFakeDb = useFakeDb;
		}
	}
}
