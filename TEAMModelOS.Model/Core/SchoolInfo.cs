using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace TEAMModelOS.Model.Core
{
	[MessagePackObject(keyAsPropertyName: true)]
	public class SchoolInfo
	{
		public string code { get; set; }
		public string name { get; set; }
		public string countryId { get; set; }
		public string countryName { get; set; }
		public string provinceId { get; set; }
		public string provinceName { get; set; }
		public string cityId { get; set; }
		public string cityName { get; set; }
		public string address { get; set; }
		public string typeId { get; set; }
		public string typeName { get; set; }
		public string source { get; set; }
		public string distId { get; set; }
		public string distName { get; set; }
		public string schoolDist { get; set; }
		public string aliasName { get; set; }
		public string shortCode { get; set; }
	}
}
