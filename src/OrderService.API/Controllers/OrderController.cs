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
    private readonly GetOrderQueryHandler _query;

    public OrdersController(CreateOrderCommandHandler handler, GetOrderQueryHandler query)
    {
        _handler = handler;
        _query = query;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = Request.Headers["X-User-Id"].ToString();
        var orders = await _query.Handle();

        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var userId = Request.Headers["X-User-Id"].ToString();
        var orderId = await _handler.Handle(command);

        return Ok(orderId);
    }
}
