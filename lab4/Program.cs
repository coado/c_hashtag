



class Program {
    private String RegionsPath = "./data/regions.csv";
    private String EmployeesPath = "./data/employees.csv";
    private String EmployeeTerritoriesPath = "./data/employee_territories.csv";
    private String TerritoriesPath = "./data/territories.csv";
    private String OrdersPath = "./data/orders.csv";
    private String OrdersDetailsPath = "./data/orders_details.csv";
    
    public List<Region> ReadRegions() {
        Reader<Region> RegionReader = new Reader<Region>();
        List<Region> regions = RegionReader.Read(RegionsPath, fields => new Region(fields[0], fields[1]));
        return regions;
    }

    public List<Territory> ReadTerritories() {
        Reader<Territory> TerritoryReader = new Reader<Territory>();
        List<Territory> territories = TerritoryReader.Read(TerritoriesPath, fields => new Territory(fields[0], fields[1], fields[2]));
        return territories;
    }

    public List<EmployeeTerritory> ReadEmployeeTerritories() {
        Reader<EmployeeTerritory> EmployeTerritoryReader = new Reader<EmployeeTerritory>();
        List<EmployeeTerritory> employeeTerritories = EmployeTerritoryReader.Read(EmployeeTerritoriesPath, fields => new EmployeeTerritory(fields[0], fields[1]));
        return employeeTerritories;
    }

    public List<Order> ReadOrders() {
        Reader<Order> OrderReader = new Reader<Order>();
        List<Order> orders = OrderReader.Read(
            OrdersPath,
            fields => new Order(
                fields[0],
                fields[1],
                fields[2],
                fields[3],
                fields[4],
                fields[5],
                fields[6],
                fields[7],
                fields[8],
                fields[9],
                fields[10],
                fields[11],
                fields[12],
                fields[13]
            )
        );

        return orders;
    }

    public List<OrderDetails> ReadOrdersDetails() {
        Reader<OrderDetails> OrdersDetailsReader = new Reader<OrderDetails>();
        List<OrderDetails> ordersDetails = OrdersDetailsReader.Read(
            OrdersDetailsPath,
            fields => new OrderDetails(
                fields[0],
                fields[1],
                fields[2],
                fields[3],
                fields[4]
            )
        );

        return ordersDetails;
    }

    public List<Employee> ReadEmployees() {
        Reader<Employee> EmployeeReader = new Reader<Employee>();
        List<Employee> employees = EmployeeReader.Read(
            EmployeesPath, 
            fields => new Employee(
                fields[0],
                fields[1],
                fields[2],
                fields[3],
                fields[4],
                fields[5],
                fields[6],
                fields[7],
                fields[8],
                fields[9],
                fields[10],
                fields[11],
                fields[12],
                fields[13],
                fields[14],
                fields[15],
                fields[16],
                fields[17]
            )
        );

        return employees;
    }
    static public void Main() {
        Program program = new Program();

        // ZAD.1
        List<Region> regions = program.ReadRegions();
        List<Employee> employees = program.ReadEmployees();
        List<Territory> territories = program.ReadTerritories();
        List<EmployeeTerritory> employeeTerritories = program.ReadEmployeeTerritories();
        List<Order> orders = program.ReadOrders();
        List<OrderDetails> ordersDetails = program.ReadOrdersDetails();

        // ZAD.2
        var results2 = from e in employees select new { lastName = e.LastName};

        Console.WriteLine("---------ZAD 2--------");
        foreach (var el in results2.ToList()) 
            Console.WriteLine(el.lastName); 
    
        // ZAD.3
        var results3 = from e in employees 
            join eTerr in employeeTerritories on e.EmployeeId equals eTerr.EmployeeId
            join t in territories on eTerr.TerritoryId equals t.TerritoryId
            join r in regions on t.RegionId equals r.RegionId
            select new { lastname = e.LastName,
                         region = r.RegionDescription,
                         territory = t.TerritoryDescription };

        Console.WriteLine("---------ZAD 3--------");
        foreach (var el in results3.ToList()) 
            Console.WriteLine($"{el.lastname}, {el.region}, {el.territory}"); 

        // ZAD.4
        var results4 = from r2 in (from r in regions
            join t in territories on r.RegionId equals t.RegionId
            join eTerr in employeeTerritories on t.TerritoryId equals eTerr.TerritoryId
            join e in employees on eTerr.EmployeeId equals e.EmployeeId
            select new { region = r, employee = e}).Distinct()
            group r2 by r2.region.RegionDescription into grouped
            select (grouped);

        Console.WriteLine("---------ZAD 4--------");
        foreach (var region in results4.ToList()) {
            Console.WriteLine($"***REGION: {region.Key}***"); 

            foreach (var e in region) {
                Console.WriteLine($"{e.employee.FirstName}, {e.employee.LastName}");
            }
        }

        // ZAD.5
        var results5 = from e in employees
            join eTerr in employeeTerritories on e.EmployeeId equals eTerr.EmployeeId
            join t in territories on eTerr.TerritoryId equals t.TerritoryId
            join r in regions on t.RegionId equals r.RegionId
            group e by r.RegionDescription into grouped
            select new { region = grouped.Key, count = grouped.Distinct().Count()};

        Console.WriteLine("---------ZAD 5--------");
        foreach (var el in results5.ToList()) 
            Console.WriteLine($"region: {el.region}, count: {el.count}");

        // ZAD.6
        var results6 = from o in orders
            join oDetails in ordersDetails on o.OrderId equals oDetails.OrderdId
            group oDetails by o.EmployeeId into grouped
            select new { 
                employee = grouped.Key, 
                count = grouped.Count(), 
                average = grouped.Average(data => data.Quantity * data.UnitPrice - data.Discount),
                max = grouped.Max(data => data.Quantity * data.UnitPrice - data.Discount)
            };
        
        Console.WriteLine("---------ZAD 6--------");
        foreach (var el in results6) 
            Console.WriteLine($"Employee: {el.employee}, Count: {el.count}, Average: {el.average}, Max: {el.max}");
    }
}

