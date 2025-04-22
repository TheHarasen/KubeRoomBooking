using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoomsApi.Controllers;
[Authorize]
[Route("rent")]
public class RentingController : ControllerBase
{

}
