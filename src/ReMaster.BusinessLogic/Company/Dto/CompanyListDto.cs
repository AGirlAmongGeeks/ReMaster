using System;
using System.Collections.Generic;
using System.Text;

namespace ReMaster.BusinessLogic.Company.Dto
{
    public class CompanyListDto
    {
		public int Id { get; set; }

		public string Name { get; set; }
		public string OwnerFullName { get; set; }

		public string Website { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string City { get; set; }

	}
}
