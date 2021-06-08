//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AceTC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Parent
    {
        [DisplayName("Identification Card No.")]
        public string parents_ic { get; set; }

        [DisplayName("Full Name")]
        public string parents_name { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string parents_pass { get; set; }

        [Required(ErrorMessage = "Password Not Match!")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("parents_pass")]
        public string confirmPass { get; set; }


        [DisplayName("E-mail")]
        public string parents_email { get; set; }

        [DisplayName("Phone No.")]
        public string parents_phone { get; set; }

        [DisplayName("Address")]
        public string parents_address { get; set; }
    }
}
