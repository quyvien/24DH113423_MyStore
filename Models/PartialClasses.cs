using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _24DH113423_MyStore.Models.ViewModel
{
    [MetadataType(typeof(UserMetadata))]
    public partial class user
    {
        [NotMapped]
        [Compare("Password")]
        public string ComfirmedPassword { get; set; }
    }
    
}