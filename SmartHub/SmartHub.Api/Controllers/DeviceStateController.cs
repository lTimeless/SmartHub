using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.DeviceState.LightState;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SmartHub.Api.Controllers
{
    public class DeviceStateController : BaseController
    {
        /// <summary>
        /// Sets the light of the given device
        /// </summary>
        /// <param name="deviceId">The given deviceId</param>
        /// <param name="toggleLight">Turn the light on or off</param>
        /// <returns></returns>
        [HttpGet("light/{deviceId}")]
        [AllowAnonymous]
        public IActionResult LightOnOff(string deviceId, [FromQuery]bool toggleLight)
        {
            var deviceLightState = new DeviceLightStateRequestDto
            {
                ToggleLight = toggleLight,
                DeviceId = deviceId
            };
            // TODO ann service schicken und dann aufrüumen
            return Ok();
        }
    }
}
