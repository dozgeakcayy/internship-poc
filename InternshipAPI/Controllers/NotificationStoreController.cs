using InternshipAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationStoreController : ControllerBase
{
    private readonly NotificationStoreService _store;

    public NotificationStoreController(NotificationStoreService store)
    {
        _store = store;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_store.GetAll());
    }
}