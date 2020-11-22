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
			[EmailAddress] //Required and needs to be in the format of an email example@email.com
			public string email { get; set; }

			[Required]
			[DataType(DataType.Password)] // Required and data display password = *********
			public string password { get; set; }

		}

}
