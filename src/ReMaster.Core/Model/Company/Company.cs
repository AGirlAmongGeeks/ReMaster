using ReMaster.Utilities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReMaster.Core.Model.Company
{
	[Table("Companies")]
	public class Company : IEntity<int> 
    {
		public int Id { get; set; }
		
		//TODO the rest properties.
    }
}
