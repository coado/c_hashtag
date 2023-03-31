

public class EmployeeTerritory {
    
    public String EmployeeId;
    public String TerritoryId;

    public EmployeeTerritory(String employeeId, String territoryId) {
        this.EmployeeId = employeeId;
        this.TerritoryId = territoryId;
    }

    public override string ToString() {
        return $"EmployeeId {EmployeeId}, TerritoryId {TerritoryId}";
    }
}