using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
	public class AddressesMetadata
	{
	}
	[MetadataType(typeof(AddressesMetadata))]
	public partial class Address
	{
		public bool isaddres { get; set; }
	}

}
