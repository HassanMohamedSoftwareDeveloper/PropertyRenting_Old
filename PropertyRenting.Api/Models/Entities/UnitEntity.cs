using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertyRenting.Api.Models.Entities
{
    [Table("Unit")]
    public class UnitEntity : BaseEntity
    {
        public string UnitNumber { get; set; }
        public string UnitName { get; set; }
        public int RoomsNumber { get; set; }
        public bool Status { get; set; }
        public int TypeId { get; set; }
        public Guid BuildingId { get; set; }
        public int Floor { get; set; }
        public bool RentStatus { get; set; }
        public DateTime ReceiveDate { get; set; }
        public decimal TotalArea { get; set; }
        public decimal RentableArea { get; set; }
        public decimal AnnualRentAmount { get; set; }
        public bool HasCentralAC { get; set; }
        public bool HasInternetService { get; set; }
        public Guid DistrictId { get; set; }
        public string AddressAR { get; set; }
        public string AddressEN { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Notes { get; set; }
        public int PathsNumber { get; set; }
        public int HallNumber { get; set; }
        public int ACNumber { get; set; }
        public int KitchenNumber { get; set; }

        [JsonIgnore]
        [ForeignKey(name: nameof(BuildingId))]
        public virtual BuildingEntity Building { get; set; }

        [JsonIgnore]
        [ForeignKey(name: nameof(DistrictId))]
        public virtual DistrictEntity District { get; set; }

        public virtual ICollection<RenterContractEntity> RenterContracts { get; set; }
    }
}
