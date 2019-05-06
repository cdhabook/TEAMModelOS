namespace TEAMModelOS.SDK.Context.Constant.Common
{
	public class FileType
	{
		public string Id { get; set;}
		public string Extention { get; set; }
		public string Type { get; set; }

		public FileType(string id, string extention, string type)
		{
			Id = id;
			Extention = extention;
			Type = type;
		}
		public static string GetExtention(string fileName) {
			if (string.IsNullOrEmpty(fileName)) {
				return "";
			} else {
				return fileName.Substring(fileName.LastIndexOf(".") + 1);
			}
			
		}
	}
}
