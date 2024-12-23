namespace LunevAPP.Models;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    // Бизнес-логика: Применение скидки к продукту
    public double ApplyDiscount(double discountPercentage)
    {
        return Price - (Price * discountPercentage / 100);
    }
}
