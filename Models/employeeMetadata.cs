using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class employeeMetadata
    {
        [Display(Name = "Name")]
        public string name;

        //[DataType(DataType.Date)]
        //public Nullable<System.DateTime> createdOn;
    }
}