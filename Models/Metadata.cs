using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _24DH113423_MyStore.Models.ViewModel
{
    public class CategoryMetadata
    {
        [HiddenInput]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string CategoryName { get; set; }
    }
    public class UserMetadata
    {
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(30, MinimumLength = 5)]
        [RegularExpression("\r\n ^[a-zA-Z0-9]([._-](?![._-])|[a-zA-Z0-9]){3,18}[a-zA-Z0-9]$ \r\n")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}