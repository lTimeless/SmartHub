using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.DeviceState.LightState;
using System.Threading.Tasks;

namespace SmartHub.Api.Controllers
{
    public class DeviceStateController : BaseController
    {
        public DeviceStateController()
        {

        }
        //TODO: remove after testing
        [AllowAnonymous]
        // GET: api/Device
        [HttpGet("light")]
        public async Task<IActionResult> LightOnOff(string pluginName)
        {
            var deviceLightState = new DeviceLightStateRequestDto { Company = "Mock", ToggleLight = true, DeviceId = "1" };
            return Ok(await Mediator.Send(new DeviceLightStateQuery(deviceLightState)));
        }
    }
}
