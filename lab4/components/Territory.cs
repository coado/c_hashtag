


public class Territory {
    public String TerritoryId;
    public String TerritoryDescription;
    public String RegionId;

    public Territory(
        String territoryId,
        String territoryDescription,
        String regionId
    ) {
        this.TerritoryId = territoryId;
        this.TerritoryDescription = territoryDescription;
        this.RegionId = regionId;
    }

    public override string ToString() {
        return $"TerritoryId {TerritoryId}, TerritoryDescription {TerritoryDescription}, RegionId {RegionId}";
    }
}