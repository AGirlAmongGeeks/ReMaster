using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReMaster.Utilities.Entities
{
    public interface IHasDateModified
	{
		//DateModified of record.
		DateTime DateModified { get; set; }
	}
}
