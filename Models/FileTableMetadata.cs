using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FileTableMetadata
    {
        public int id { get; set; }
        [Display(Name ="Name")]
        public string namefile { get; set; }
        public string path { get; set; }
    }
    [MetadataType(typeof(FileTableMetadata))]
    public partial class FileTable
    {
        public HttpPostedFileBase[] ImageFile { get; set; }
    }

}