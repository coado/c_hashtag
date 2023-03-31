

public class Employee {
    public String EmployeeId;
    public String LastName;
    public String FirstName;
    public String Title;
    public String TitleOfCourtesy;
    public String BirthDate;
    public String HireDate;
    public String Address;
    public String City;
    public String Region;
    public String PostalCode;
    public String Country;
    public String HomePhone;
    public String Extension;
    public String Photo;
    public String Notes;
    public String Reportsto;
    public String PhotoPath;

    public Employee(
        String employeeId,
        String lastname,
        String firstname,
        String title,
        String titleOfCourtesy,
        String birthdate,
        String hiredate,
        String address,
        String city,
        String region,
        String postalcode,
        String country,
        String homephone,
        String extension,
        String photo,
        String notes,
        String reportsto,
        String photoPath
    ) {
        this.EmployeeId = employeeId;
        this.Address = address;
        this.BirthDate = birthdate;
        this.City = city;
        this.Country = country;
        this.Extension = extension;
        this.FirstName = firstname;
        this.HireDate = hiredate;
        this.HomePhone = homephone;
        this.LastName = lastname;
        this.Notes = notes;
        this.Photo = photo;
        this.PhotoPath = photoPath;
        this.PostalCode = postalcode;
        this.Region = region;
        this.Reportsto = reportsto;
        this.Title = title;
        this.TitleOfCourtesy = titleOfCourtesy;
    } 

    public override string ToString() {
        return $"EmployeeId {EmployeeId}, LastName {LastName}, FirstName {FirstName}";
    }
}

