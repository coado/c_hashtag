


public class Region {
    public String RegionId;
    public String RegionDescription;

    public Region(String regionId, String regionDesc) {
        this.RegionId = regionId;
        this.RegionDescription = regionDesc;
    }

    public override string ToString() {
        return $"regionId: {this.RegionId}, RegionDescription: {this.RegionDescription}";
    }
}