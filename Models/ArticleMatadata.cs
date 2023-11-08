using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ArticleMatadata
    {
        [Display(Name = "ชื่อบทความ")]
        [StringLength(20)]
        [Required(ErrorMessage="Please enter Title.")]
        public string title;
        
        [StringLength(200)]
        [Required(ErrorMessage="Please enter Article.")]
        public string value;
    }

    [MetadataType(typeof(ArticleMatadata))]
    public partial class Article
    {
        public string dateandtitle { get; set; }

        public void setdateandtitle()
        {
            this.dateandtitle = this.date + " " + this.title; 
        }
    }
}