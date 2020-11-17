using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project2_Dimention_Data.Models.Entities
{
    [ModelMetadataType(typeof(UserMetaDataType))]
    public partial class Login
    {

    }

    public partial class UserMetaDataType
    {
        [Required, StringLength(50, MinimumLength =10)]
        public string Passwordhash { get; set; }

        [Required, StringLength(50, MinimumLength =5)]
        [Display(Name = "First Name")]
        public string NameUser { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        [Display(Name = "Surname")]
        public string SurnameUser { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string UserEmail { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        public string UserRole { get; set; }
    }
}
