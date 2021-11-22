using HealthAndBeauty.Models;
using HealthAndBeauty.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Data.Repositories
{
    public class OrdersRepository
    {
        private ApplicationDbContext context;
        public OrdersRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        internal List<Order> GetOrders() => 
             context.Orders.ToList();

        internal List<Order> GetOrdersByCourierId(Guid courierId) =>
            context.Orders.Where(order => order.CourierId == courierId).OrderBy(order => order.Id).ToList();
        

        internal int AddOrderItems(OrderItem orderItem)
        {
            context.OrderItems.Add(orderItem);
            context.SaveChanges();
            return orderItem.Id;
        }

        internal int AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order.Id;
        }

        internal void UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }

        internal Order GetOrder(string orderId) => 
            context.Orders.Where(order => order.Id == int.Parse(orderId)).First();
        
    }
}
