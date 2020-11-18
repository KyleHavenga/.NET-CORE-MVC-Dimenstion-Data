using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project2_Dimention_Data.Models.ViewModels
{
 

		public class AccountLogin
		{
			[Required]
			[EmailAddress]
			public string email { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string password { get; set; }

		}

}
