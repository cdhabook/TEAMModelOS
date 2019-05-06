using System.Collections.Generic;


namespace TEAMModelOS.SDK.Context.Constant.Common
{
	public class FileTypeMap
	{
		private Dictionary<string, FileType> FileTpyeMap { get; set; }

		public FileTypeMap FileTypes(FileType fileType) {
			if (this.FileTpyeMap == null)
			{
				FileTpyeMap = new Dictionary<string, FileType>();
				//fileTpyeMap.Add(fileType.Type, fileType);
			}
			if(!FileTpyeMap.ContainsKey(fileType.Extention)) {
				FileTpyeMap.Add(fileType.Extention, fileType);
			}
			return this;
		}
		public Dictionary<string, FileType> GetFileTypes() {
			FileTypes(new FileType("1", "jpg", "img")).
			FileTypes(new FileType("2", "png", "img")).
			FileTypes(new FileType("3", "bmp", "img")).
			FileTypes(new FileType("4", "jpeg", "img")).
			FileTypes(new FileType("5", "gif", "img")).
			FileTypes(new FileType("6", "mp4", "video")).
			FileTypes(new FileType("7", "avi", "video")).
			FileTypes(new FileType("8", "mpeg", "video")).
			FileTypes(new FileType("9", "mov", "video")).
			FileTypes(new FileType("10", "wmv", "video")).
			FileTypes(new FileType("11", "doc", "file")).
			FileTypes(new FileType("12", "docx", "file")).
			FileTypes(new FileType("13", "ppt", "file")).
			FileTypes(new FileType("14", "pptx", "file")).
			FileTypes(new FileType("15", "xls", "file")).
			FileTypes(new FileType("16", "xlsx", "file")).
			FileTypes(new FileType("17", "zip", "file")).
			FileTypes(new FileType("18", "rar", "file")).
			FileTypes(new FileType("19", "pdf", "file"));
			return FileTpyeMap;
		}
	}
}
