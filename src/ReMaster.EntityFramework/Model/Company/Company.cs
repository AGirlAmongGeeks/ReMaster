using ReMaster.Utilities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReMaster.EntityFramework.Model
{
	[Table("Companies")]
	public class Company : IEntity<int> 
    {
		public int Id { get; set; }

		public string CeidgId { get; set; }
		public string Name { get; set; }
		public string OwnerFirstName { get; set; }
		public string OwnerLastName { get; set; }
		public string NIP { get; set; }
		public string Regon { get; set; }

		public string Website { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }

		public string City { get; set; }
		public string BuildingNo { get; set; }
		public string PostalCode { get; set; }
		public string PostalCity { get; set; }
		public string District { get; set; }
		public string Voivodeship { get; set; }
		public string AddessAnomaly { get; set; }
		public string Status { get; set; }
		public string PKDCodes { get; set; }
	}
}
