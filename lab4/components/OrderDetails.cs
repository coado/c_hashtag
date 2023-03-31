


public class OrderDetails {
    public String OrderdId;
    public String ProductId;
    public double UnitPrice;
    public double Quantity;
    public double Discount;

    public OrderDetails(
        String orderId,
        String productId,
        String unitPrice,
        String quantity,
        String discount
    ) {
        this.OrderdId = orderId;
        this.ProductId = productId;
        this.UnitPrice = double.Parse(unitPrice);
        this.Quantity = double.Parse(quantity);
        this.Discount = double.Parse(discount);
    }

    public override string ToString() {
        return $"OrderdId {OrderdId}, ProductId {ProductId}";
    }
}