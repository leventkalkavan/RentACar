using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Car : BaseEntity<Guid>
{
    public Car()
    {
    }

    public Car(Guid id, Guid modelId, CarState carState, int km, short modelYear, short minFindexScore,
        string plate) : this()
    {
        Id = id;
        ModelId = modelId;
        CarState = carState;
        Km = km;
        ModelYear = modelYear;
        Plate = plate;
        MinFindexScore = minFindexScore;
    }

    public Guid ModelId { get; set; }
    public int Km { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindexScore { get; set; }
    public CarState CarState { get; set; }
    public virtual Model? Model { get; set; }
}