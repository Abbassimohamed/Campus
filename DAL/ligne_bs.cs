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
    
    public partial class ligne_bs
    {
        public int id { get; set; }
        public Nullable<int> id_bs { get; set; }
        public string code_art { get; set; }
        public string designation_article { get; set; }
        public Nullable<double> quantite_article { get; set; }
        public Nullable<double> puv { get; set; }
        public Nullable<double> remise { get; set; }
        public Nullable<double> prixRemise { get; set; }
        public Nullable<double> total { get; set; }
    }
}
