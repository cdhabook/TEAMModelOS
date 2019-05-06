using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEAMModelOS.SDK.Extension.DataResult.JsonRpcRequest;
using TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse;
using TEAMModelOS.Model.Core;
using TEAMModelOS.SDK.Helper.Common.ValidateHelper;
using TEAMModelOS.Service.Core.Implements;
using TEAMModelOS.Service.Core.Interfaces;

namespace TEAMModelOS.Admin.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommonController : Controller
	{
		private readonly ISchoolsService _schoolsService;
		public CommonController(ISchoolsService schoolsService ) {
			_schoolsService = schoolsService;
		}
		[HttpPost("getSchool")]
		public async Task<BaseJosnRPCResponse> GetSchoolAsync(JosnRPCRequest<FindSchoolByCode> getSchool)
		{

			JsonRPCResponseBuilder builder = JsonRPCResponseBuilder.custom();
			if (ValidateHelper.IsValid(getSchool))
			{
				List<SchoolInfo> schoolInfos = await _schoolsService.GetSchools(getSchool);
				if (schoolInfos != null && schoolInfos.Count > 0)
				{
					builder.Data(schoolInfos);
				}
				else builder.Data(null);
			}
			return builder.build();

		}
	}
}
