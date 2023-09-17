namespace CoffeeShop.POS.Models;

public class Order
{
    public int OrderId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}
