using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project2_Dimention_Data.Models.ViewModels
{

    
		public class UserManagementCreate : UserManagementChangePassword
		{
			[Required, StringLength(50, MinimumLength = 2)]
			[Display(Name = "First Name")]
			public string NameUser { get; set; }

			[Required, StringLength(50, MinimumLength = 2)]
			[Display(Name = "Last Name")]
			public string SurnameUser { get; set; }

		    [Required, StringLength(50, MinimumLength = 2)]
	     	[Display(Name = "Last Name")]
	     	public string PasswordHash { get; set; }

		    [Required, EmailAddress, StringLength(100)]
			public string UserEmail { get; set; }

		    [Required, ]
		    public string EmpNum { get; set; }

		    public string PasswordSalt { get; set; }

		    public string UserRole { get; set; }

		    public string id { get; set; }
	}

		public class UserManagementChangePassword
		{
			[Required, StringLength(15, MinimumLength = 6)]
			[DataType(DataType.Password)]
			public string Passwordhash { get; set; }

			[Required, StringLength(15, MinimumLength = 6)]
			[Compare("PasswordHash")]
			[Display(Name = "Confirm Password")]
			[DataType(DataType.Password)]
			public string ConfirmPassword { get; set; }
		}
	
}
