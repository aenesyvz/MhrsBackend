using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Patient : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string NationalityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ImageUrl { get; set; }
    public int Height { get; set; }
    public double Weight { get; set; }
    public decimal? BodyMassIndex { get; set; }
    public DateTime DateOfBirth { get; set; }


    public virtual User User { get; set; }
    //public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }

    public Patient()
    {
        Prescriptions = new HashSet<Prescription>();
    }


    public Patient(Guid id, Guid userId, string nationalityNumber, DateTime dateOfBirth, string? imageUrl, double weight, int height, decimal? bodyMassIndex) : this()
    {
        Id = id;
        UserId = userId;
        NationalityNumber = nationalityNumber;
        DateOfBirth = dateOfBirth;
        ImageUrl = imageUrl;
        Weight = weight;
        Height = height;
        BodyMassIndex = bodyMassIndex;
    }
}