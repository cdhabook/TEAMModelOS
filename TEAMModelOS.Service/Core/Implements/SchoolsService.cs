using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.Model.Core;
using TEAMModelOS.SDK.Context.Configuration;
using TEAMModelOS.SDK.Extension.DataResult.JsonRpcRequest;
using TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse;
using TEAMModelOS.SDK.Extension.HttpClient.Implements;
using TEAMModelOS.SDK.Helper.Common.JsonHelper;
using TEAMModelOS.Service.Core.Interfaces;

namespace TEAMModelOS.Service.Core.Implements
{
	public class SchoolsService : ISchoolsService
	{
		private readonly HttpClientSchool _httpClientSchool;
		public SchoolsService(HttpClientSchool httpClientSchool) {
			_httpClientSchool = httpClientSchool;
		}
		public async Task<List<SchoolInfo>> GetSchools(JosnRPCRequest<FindSchoolByCode> getSchool)
		{
			string schoolString = await RequestSchoolInfo(getSchool);
			if (!string.IsNullOrEmpty(schoolString))
			{
				JosnRPCResponse<List<SchoolInfo>> response = MessagePackHelper.JsonToObject<JosnRPCResponse<List<SchoolInfo>>>(schoolString);
				if (response.error == null && response != null)
				{
					List<SchoolInfo> schoolInfo = response.result;
					return schoolInfo;
				}
				else return null;

			}
			else return null;
		}

		private async Task<string> RequestSchoolInfo(JosnRPCRequest<FindSchoolByCode> getSchool) {
			string SchoolCodeKey = BaseConfigModel.Configuration["HaBookAuth:SchoolCodeKey"];
			Dictionary<string, string> dict = new Dictionary<string, string>
			{
				{ "Authorization", SchoolCodeKey }
			};
			Dictionary<string, object> codeInfo = new Dictionary<string, object>
			{
				{ "countryId", getSchool.@params.CountryId },
				{ "provinceId", getSchool.@params.ProvinceId },
				{ "cityId", getSchool.@params.CityId }
			};
			Dictionary<string, object> data = new Dictionary<string, object>
			{
				{ "data", codeInfo }
			};
			JosnRPCRequest<Dictionary<string, object>> request = new JosnRPCRequest<Dictionary<string, object>>
			{
				@params = data,
				method = "SchoolCode"
			};
			string postData = MessagePackHelper.ObjectToJson(request);
			return await _httpClientSchool.HttpPostAsync(BaseConfigModel.Configuration["HaBookAuth:ServiceUrl"], postData);
		}
	}
}
