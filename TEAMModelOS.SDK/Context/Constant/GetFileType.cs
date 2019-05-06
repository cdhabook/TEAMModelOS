using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Context.Constant.Common
{
	public class GetFileType
	{
		//private 

		public GetFileType() { 
		
		}

		public string GetExtension(string fileName) {
			return fileName.Substring(fileName.LastIndexOf(".") + 1, (fileName.Length - fileName.LastIndexOf(".") - 1)); //扩展名
		}

		public string GetType(string fileName) {
			string extension = GetExtension(fileName);

			return null;
		}
	}
}
