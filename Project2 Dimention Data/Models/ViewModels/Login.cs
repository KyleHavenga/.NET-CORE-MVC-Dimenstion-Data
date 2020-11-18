using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project2_Dimention_Data.Models.Entities
{
    [ModelMetadataType(typeof(UserMetaDataType))]
    public partial class Login
    {

    }

    public partial class UserMetaDataType : UserManagementChangePassword
    {
		[Required, StringLength(50, MinimumLength = 2)]
		[Display(Name = "First Name")]
		public string NameUser { get; set; }

		[Required, StringLength(50, MinimumLength = 2)]
		[Display(Name = "Last Name")]
		public string SurnameUser { get; set; }

		[Required, EmailAddress, StringLength(100)]
		public string UserEmail { get; set; }

		[Required]
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
		[Compare("Password")]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
