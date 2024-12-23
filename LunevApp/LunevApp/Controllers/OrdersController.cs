/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LunevAPP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LunevAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // Получить все заказы
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems)
                                               .ThenInclude(oi => oi.Product)
                                               .ToListAsync();
            return Ok(orders);
        }

        // Получить заказ по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Product)
                                              .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            return Ok(order);
        }

        // Создать новый заказ с продуктами
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request == null || request.UserId <= 0 || request.Products == null || !request.Products.Any())
            {
                return BadRequest("Invalid request. Ensure that user ID and products are provided.");
            }

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Создаем новый заказ
            var newOrder = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.UtcNow
                
            };

            // Добавляем продукты в заказ
            foreach (var productRequest in request.Products)
            {
                var product = await _context.Products.FindAsync(productRequest.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID {productRequest.ProductId} not found.");
                }

                // Добавляем продукт в заказ
                newOrder.AddProduct(product, productRequest.Quantity);
            }

            // Сохраняем заказ и его элементы в базе
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }

        // Добавить товар в заказ
        [HttpPost("{orderId}/add-product")]
        public async Task<IActionResult> AddProductToOrder(int orderId, [FromBody] AddProductRequest request)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Product)
                                              .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Добавление товара в заказ
            order.AddProduct(product, request.Quantity);

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        // Применить скидку к продукту
        [HttpPost("{orderId}/apply-discount")]
        public async Task<IActionResult> ApplyDiscountToProduct(int orderId, [FromBody] ApplyDiscountRequest request)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Product)
                                              .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            var discountedPrice = product.ApplyDiscount(request.DiscountPercentage);

            return Ok(new { DiscountedPrice = discountedPrice });
        }
    }

    // DTO для создания заказа
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public List<OrderProductRequest> Products { get; set; }  // Добавляем список продуктов
    }

    // DTO для представления продукта в запросе
    public class OrderProductRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // DTO для добавления продукта в заказ
    public class AddProductRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // DTO для применения скидки
    public class ApplyDiscountRequest
    {
        public int ProductId { get; set; }
        public double DiscountPercentage { get; set; }
    }
}
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LunevAPP.Models;
using System.Linq;
using System.Threading.Tasks;


namespace LunevAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // Получить все заказы
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems)
                                               .ThenInclude(oi => oi.Product)
                                               .ToListAsync();
            return Ok(orders);
        }

        // Получить заказ по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Product)
                                              .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            return Ok(order);
        }

        // Создать новый заказ с продуктами
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request == null || request.UserId <= 0 || request.Products == null || !request.Products.Any())
            {
                return BadRequest("Invalid request. Ensure that user ID and products are provided.");
            }

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Создаем новый заказ
            var newOrder = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.UtcNow
            };

            // Добавляем продукты в заказ
            foreach (var productRequest in request.Products)
            {
                var product = await _context.Products.FindAsync(productRequest.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID {productRequest.ProductId} not found.");
                }

                // Добавляем продукт в заказ
                newOrder.AddProduct(product, productRequest.Quantity);
            }

            // Обновляем цену заказа
            newOrder.UpdatePrice();

            // Сохраняем заказ и его элементы в базе
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }

        // Добавить товар в заказ
        [HttpPost("{orderId}/add-product")]
        public async Task<IActionResult> AddProductToOrder(int orderId, [FromBody] AddProductRequest request)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Product)
                                              .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Добавление товара в заказ
            order.AddProduct(product, request.Quantity);

            // Обновляем цену заказа
            order.UpdatePrice();

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        // Применить скидку к продукту
        [HttpPost("{orderId}/apply-discount")]
        public async Task<IActionResult> ApplyDiscountToProduct(int orderId, [FromBody] ApplyDiscountRequest request)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Product)
                                              .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            var discountedPrice = product.ApplyDiscount(request.DiscountPercentage);

            return Ok(new { DiscountedPrice = discountedPrice });
        }
    }

    // DTO для создания заказа
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public List<OrderProductRequest> Products { get; set; }  // Добавляем список продуктов
    }

    // DTO для представления продукта в запросе
    public class OrderProductRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // DTO для добавления продукта в заказ
    public class AddProductRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // DTO для применения скидки
    public class ApplyDiscountRequest
    {
        public int ProductId { get; set; }
        public double DiscountPercentage { get; set; }
    }
}
