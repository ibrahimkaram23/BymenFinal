using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BymenFinal.Models
{
    public class Delegation
    {
        public string[] fullName { get; set; }
        public string[] emailAddress { get; set; }
        public string[] university { get; set; }
        public string[] eduYear { get; set; }
        public string[] eduMajor { get; set; }
        public string[] country { get; set; }
        public string[] conferaneModel { get; set; }
        public string[] committee { get; set; }
        public string[] age { get; set; }
        public string[] enrolledAs { get; set; }
        public string[] gender { get; set; }
        public string[] nationality { get; set; }
        public string[] countryCode { get; set; }
        public string[] phoneNb { get; set; }
        public string[] isVaccinated { get; set; }
        public string[] doses { get; set; }
        public string[] reference { get; set; }
        public HttpPostedFileBase[] profilePic { get; set; }
        public HttpPostedFileBase[] passportPict { get; set; }
        public HttpPostedFileBase[] vaccinationCert { get; set; }
    }
}