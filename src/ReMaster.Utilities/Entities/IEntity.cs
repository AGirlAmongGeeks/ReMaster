using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReMaster.Utilities.Entities
{
    public interface IEntity<TPrimaryKey>
	{
		TPrimaryKey Id { get; set; }
	}
}
