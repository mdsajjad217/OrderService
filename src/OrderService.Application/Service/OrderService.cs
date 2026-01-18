using OrderService.Application.Event;
using OrderService.Application.Repository;
using OrderService.Domain.Entity;
using OrderService.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IEventPublisher _eventPublisher;

        public OrderService(IOrderRepository orderRepository,
            IEventPublisher eventPublisher)
        {
            _orderRepository = orderRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task SaveAsync(Order order)
        {
        //    try
        //    {
        //        await _orderRepository.SaveAsync(order);

        //        var evt = new OrderCreatedEvent
        //        {
        //            OrderId = order.Id,
        //            ProductName = order.ProductName,
        //            Amount = order.Amount,
        //            CreatedAt = order.CreatedAt
        //        };

        //        await _eventPublisher.PublishAsync("order-created", evt);

        //        await _context.SaveChangesAsync();

        //        await using var transaction =
        //await _dbContext.Database.BeginTransactionAsync();

        //        try
        //        {
        //            await _repo1.AddAsync(entity1);
        //            await _repo2.AddAsync(entity2);

        //            await _dbContext.SaveChangesAsync();
        //            await transaction.CommitAsync();
        //        }
        //        catch
        //        {
        //            await transaction.RollbackAsync();
        //            throw;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        }
    }
}
