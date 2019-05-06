using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public  class JsonRPCResponseBuilder
    {
        private string message="Success";
        private string devmsg = "Error";
        private int code = 0;
        private object data;
        private long total;
        private int currPage;
        private int pageSize;
        private int totalPage;
        private Dictionary<string, object> extend;
        private Pagination page;
        private AzureTableToken token;
        private object error=null;
        
        public JsonRPCResponseBuilder()
        {
        }
        public JsonRPCResponseBuilder Success()
        {
            error = null;
            return this;
        }

        public JsonRPCResponseBuilder Success(String message)
        {
            this.message = message;
            return this;
        }
        public static JsonRPCResponseBuilder custom()
        {
            return new JsonRPCResponseBuilder();
        }

        public JsonRPCResponseBuilder Data(object data)
        {
            this.data = data;
            return this;
        }
		public JsonRPCResponseBuilder Error(object error, string message)
		{
			this.code = 1;
			this.message = message;
			this.error = error;
			return this;
		}
		public JsonRPCResponseBuilder Error(object error, int code,string message)
		{
			this.code = code;
			this.message = message;
			this.error = error;
			return this;
		}
		public JsonRPCResponseBuilder Error(object error,int code)
		{
			this.code = code;
			this.message = "Error";
			this.error = error;
			return this;
		}
		public JsonRPCResponseBuilder Error(object error)
        {
			this.code = 1;
			this.message = "Error";
            this.error = error;
            return this;
        }
        public JsonRPCResponseBuilder Extend(Dictionary<String, object> extend)
        {
            this.extend = extend;
            return this;
        }
        public JsonRPCResponseBuilder Token(AzureTableToken token)
        {
            this.token = token;
            return this;
        }
        public JsonRPCResponseBuilder Page(Pagination page)
        {
            this.pageSize = page.pageSize;
            this.currPage = page.currPage;
            this.total = page.total;
            this.page = page;
            this.totalPage = (int)Math.Ceiling((double)this.total / (double)this.pageSize);
            return this;
        }
        public JsonRPCResponseBuilder totalCount(int totalCount)
        {
            this.total = totalCount;
            return this;
        }

        public JsonRPCResponseBuilder CurrPage(int currPage)
        {
            this.currPage = currPage;
            return this;
        }

        public JsonRPCResponseBuilder PageSize(int pageSize)
        {
            this.pageSize = pageSize;
            return this;
        }

        public JsonRPCResponseBuilder TotalPage(int totalPage)
        {
            this.totalPage = totalPage;
            return this;
        }
        public BaseJosnRPCResponse build()
        {
            object baseResponse= null;

            if (error != null) {
                ErrorJosnRPCResponse<object> errorJosnRPCResponse = new ErrorJosnRPCResponse<object>();
                errorJosnRPCResponse.error.code = code;
                errorJosnRPCResponse.error.message = message;
                errorJosnRPCResponse.error.data = error;
                errorJosnRPCResponse.error.devmsg = devmsg;
                baseResponse = errorJosnRPCResponse;
                return (BaseJosnRPCResponse)baseResponse;
            }
            if (this.total > 0 && this.pageSize > 0)
            {
                this.totalPage = (int)Math.Ceiling((double)this.total / (double)this.pageSize);
            }
            if (null != this.data && this.token != null)
            {
                TokenJosnRPCResponse<object> tokenJosnRPCResponse = new TokenJosnRPCResponse<object>();
                tokenJosnRPCResponse.result.data = this.data;
                tokenJosnRPCResponse.result.extend = this.extend;
                tokenJosnRPCResponse.result.azureToken = this.token;
                tokenJosnRPCResponse.result.message = message;
                baseResponse = tokenJosnRPCResponse;
            }
            else if (null != this.data && this.total > 0 && this.currPage > 0 && this.pageSize > 0 && this.totalPage > 0)
            {
                PageJosnRPCResponse<object> pageDatasResponse = new PageJosnRPCResponse<object>();
                pageDatasResponse.result.data = this.data;
                pageDatasResponse.result.page = new Pagination(this.total, this.currPage, this.pageSize, this.totalPage);
                pageDatasResponse.result.extend = this.extend;
                pageDatasResponse.result.message = message;
                baseResponse = pageDatasResponse;
            }
            else if (this.data != null)
            {
                DataJosnRPCResponse<object> datasResponse = new DataJosnRPCResponse<object>();
                datasResponse.result.data = this.data;
                datasResponse.result.extend = this.extend;
                datasResponse.result.message = message;
                baseResponse = datasResponse;
            }
            else
            {
                baseResponse = new BaseJosnRPCResponse();
            }
            return (BaseJosnRPCResponse)baseResponse;
        }
    }
}
