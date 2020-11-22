using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project2_Dimention_Data.Models.ViewModels
{

    
		public class UserManagementCreate : UserManagementChangePassword
		{
			[Required, StringLength(50, MinimumLength = 2)] //Required with a max length of 50 and minimum lenght of 2
			[Display(Name = "First Name")]
			public string NameUser { get; set; }

			[Required, StringLength(50, MinimumLength = 2)] //Required with a max length of 50 and minimum lenght of 2
		    [Display(Name = "Last Name")]
			public string SurnameUser { get; set; }

		    [Required, StringLength(50, MinimumLength = 2)] //Required with a max length of 50 and minimum lenght of 2
	     	[Display(Name = "Last Name")]
	     	public string PasswordHash { get; set; }

		    [Required, EmailAddress, StringLength(100)] // needs to be an email is required and has a string lenght of 100
			public string UserEmail { get; set; }

		    [Required, ] // Required
		    public string EmpNum { get; set; }

		    public string PasswordSalt { get; set; }

		    public string UserRole { get; set; }

		    public string id { get; set; }
	}

		public class UserManagementChangePassword
		{
			[Required, StringLength(15, MinimumLength = 6)] // Required and string max lenght of 15 and minimum lenght of 6
			[DataType(DataType.Password)] // data display password = *********
		    public string Passwordhash { get; set; }

			[Required, StringLength(15, MinimumLength = 6)] // Required and string max lenght of 15 and minimum lenght of 6
		    [Compare("PasswordHash")] // Compares to the password hash
			[Display(Name = "Confirm Password")] // data display password = *********
		    [DataType(DataType.Password)]
			public string ConfirmPassword { get; set; }
		}
	
}
