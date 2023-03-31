

public class Order {
    public String OrderId;
    public String CustomerId;
    public String EmployeeId;
    public String OrderDate;
    public String RequiredDate;
    public String ShippedDate;
    public String Shipvia;
    public String Freight;
    public String ShipName;
    public String ShipAddress;
    public String ShipCity;
    public String ShipRegion;
    public String ShipPostalCode;
    public String ShipCountry;

    public Order(
        String orderId,
        String customerId,
        String employeeId,
        String orderDate,
        String requiredDate,
        String shippedDate,
        String shipvia,
        String freight,
        String shipName,
        String shipAddress,
        String shipCity,
        String shipRegion,
        String shipPostCode,
        String shipCountry
    ) {
        this.OrderId = orderId;
        this.CustomerId = customerId;
        this.EmployeeId = employeeId;
        this.OrderDate = orderDate;
        this.RequiredDate = requiredDate;
        this.ShippedDate = shippedDate;
        this.Shipvia = shipvia;
        this.Freight = freight;
        this.ShipName = shipName;
        this.ShipAddress = shipAddress;
        this.ShipCity = shipCity;
        this.ShipRegion = shipRegion;
        this.ShipPostalCode = shipPostCode;
        this.ShipCountry = shipCountry;
    }

    public override string ToString() {
        return $"OrderId {this.OrderId}, CustomerId {CustomerId}, EmployeeId {EmployeeId}";
    }
}