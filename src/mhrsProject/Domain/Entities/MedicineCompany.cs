using Core.Persistence.Repositories;

namespace Domain.Entities;

public class MedicineCompany : Entity<Guid>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxOffice { get; set; }
    public string TaxNumber { get; set; }
    public string? ImageUrl { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; }

    public MedicineCompany()
    {
        Medicines = new HashSet<Medicine>();
    }

    public MedicineCompany(Guid id, string name, string addres, string email, string phoneNumber, string taxOffice, string taxNumber, string? imageUrl) : this()
    {
        Id = id;
        Name = name;
        Address = addres;
        Email = email;
        PhoneNumber = phoneNumber;
        TaxOffice = taxOffice;
        TaxNumber = taxNumber;
        ImageUrl = imageUrl;
    }

}