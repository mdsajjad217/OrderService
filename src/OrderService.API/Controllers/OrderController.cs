// Namespace: OrderService.API.Controllers
namespace OrderService.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Command;
using OrderService.Application.Handler;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderCommandHandler _handler;

    public OrdersController(CreateOrderCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var orderId = await _handler.Handle(command);
        return Ok(orderId);
    }
}
