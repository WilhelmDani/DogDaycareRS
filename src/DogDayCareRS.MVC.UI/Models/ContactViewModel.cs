using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DogDayCareRS.MVC.UI.Models
{
    public class ContactViewModel
    {

        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Unrecognized: Please check Email for spelling errors")]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required(ErrorMessage = "*")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}