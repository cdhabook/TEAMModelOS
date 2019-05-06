using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.Model.Core;
using TEAMModelOS.SDK.Extension.DataResult.JsonRpcRequest;

namespace TEAMModelOS.Service.Core.Interfaces
{
	public interface ISchoolsService : IBusinessService
	{
		Task<List<SchoolInfo>> GetSchools(JosnRPCRequest<FindSchoolByCode> codeInfo);
	}
}
