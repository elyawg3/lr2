/*using Microsoft.EntityFrameworkCore.Metadata;

namespace LunevAPP.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    

    // Бизнес-логика: Добавление продукта в заказ
    public void AddProduct(Product product, int quantity)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        }
        OrderItems.Add(new OrderItem { Product = product, Quantity = quantity });
    }
    public double Price
    {
        get
        {
            // Суммируем стоимость всех товаров в заказе
            return OrderItems.Sum(oi => oi.Product.Price * oi.Quantity);
        }
    }
}

*/

namespace LunevAPP.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Добавим поле Price, которое будет храниться в базе данных
        public double Price { get; set; }

        // Бизнес-логика: Добавление продукта в заказ
        public void AddProduct(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }
            OrderItems.Add(new OrderItem { Product = product, Quantity = quantity });
        }

        // Метод для обновления общей стоимости заказа
        public void UpdatePrice()
        {
            Price = OrderItems.Sum(oi => oi.Product.Price * oi.Quantity);
        }
    }
}
