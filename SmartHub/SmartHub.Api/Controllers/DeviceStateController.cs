using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.DeviceState.LightState;
using System.Threading.Tasks;

namespace SmartHub.Api.Controllers
{
    public class DeviceStateController : BaseController
    {
        //TODO: remove after testing
        [AllowAnonymous]
        // GET: api/Device
        [HttpGet("light/{deviceId}/{toggleLight}/{company}")]
        public async Task<IActionResult> LightOnOff(string deviceId, bool toggleLight, string company)
        {
            var deviceLightState = new DeviceLightStateRequestDto
            {
                Company = company,
                ToggleLight = toggleLight,
                DeviceId = deviceId
            };
            return Ok(await Mediator.Send(new DeviceLightStateQuery(deviceLightState)));
        }
    }
}
