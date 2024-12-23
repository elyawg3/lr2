namespace LunevAPP.Models;
public class OrderItem
{
    public int Id { get; set; }
    public Order? Order;// Связь с заказом

    public int ProductId { get; set; }
    public Product? Product;// Связь с продуктом

    public int Quantity { get; set; }
}
