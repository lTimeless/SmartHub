namespace SmartHub.Application.Common.Models
{
	public class ExceptionPayload : Payload
	{
		public ExceptionPayload(UserError error) : base(new []{ error })
		{
		}
	}
}