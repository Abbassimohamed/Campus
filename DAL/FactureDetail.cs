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
    
    public partial class FactureDetail
    {
        public int id { get; set; }
        public Nullable<int> nfacture { get; set; }
        public Nullable<int> nligne { get; set; }
        public string date { get; set; }
        public Nullable<int> client { get; set; }
        public string article { get; set; }
        public Nullable<double> quantite { get; set; }
        public Nullable<double> pu { get; set; }
    }
}
