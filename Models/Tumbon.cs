//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tumbon
    {
        public int id { get; set; }
        public Nullable<int> zip_code { get; set; }
        public string name_th { get; set; }
        public string name_en { get; set; }
        public int amphure_id { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<bool> deleted_at { get; set; }
    
        public virtual Amphure Amphure { get; set; }
    }
}
