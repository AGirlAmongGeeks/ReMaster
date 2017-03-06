using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReMaster.Utilities.Entities
{
    public interface IHasDateCreated 
	{
		//DateCreated of record.
		DateTime DateCreated { get; set; }
	}
}
