//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class societe
    {
        public int id { get; set; }
        public string rsoc { get; set; }
        public string adresse { get; set; }
        public string codpost { get; set; }
        public string ville { get; set; }
        public string mobile { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public string mail { get; set; }
        public string siteweb { get; set; }
        public byte[] logo { get; set; }
        public string assujetitva { get; set; }
        public Nullable<double> numtva { get; set; }
        public string activite { get; set; }
        public string nomresp { get; set; }
        public string formejuridique { get; set; }
        public string matfisc { get; set; }
    }
}