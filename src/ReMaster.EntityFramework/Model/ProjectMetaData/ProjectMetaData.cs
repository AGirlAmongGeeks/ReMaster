using ReMaster.Utilities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReMaster.EntityFramework.Model
{
	[Table("ProjectMetaDatas")]
	public class ProjectMetaData//: IEntity<int>
    {
		//public int Id { get; set; }

		[Key]
		public string Key { get; set; }
		public string Value { get; set; }

	}
}
