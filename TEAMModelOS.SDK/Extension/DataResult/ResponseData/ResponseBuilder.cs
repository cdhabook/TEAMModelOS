using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using MessagePack;
using System;
using System.Collections.Generic;


namespace TEAMModelOS.SDK.Extension.DataResult.ResponseData
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class ResponseBuilder
    {
        private string message = "success";
        private object data;
        private int code = 0;
       // private IList<object> datas;
        private long total;
        private int currPage;
        private int pageSize;
        private int totalPage;
        private Dictionary<string, object> extend;
        private Pagination page;
        private AzureTableToken  token;
        public ResponseBuilder()
        {
        }

        public static ResponseBuilder custom()
        {
            return new ResponseBuilder();
        }

        public BaseResponse build()
        {
            if (this.total > 0 && this.pageSize > 0 )
            {
                this.totalPage = (int)Math.Ceiling((double)this.total / (double)this.pageSize);
            }
            object baseResponse;
           
             if (null != this.data && this.token!=null)
            {
                TokenPageDatasResponse<object> pageDatasResponse = new TokenPageDatasResponse<object>();
                pageDatasResponse.code= this.code;
                pageDatasResponse.data= this.data;
                pageDatasResponse.message= this.message;
                pageDatasResponse.extend=this.extend;
                pageDatasResponse.azureToken = this.token;
                baseResponse = pageDatasResponse;
            }
            else if (null != this.data && this.total > 0 && this.currPage > 0 && this.pageSize > 0 && this.totalPage > 0)
            {
                PageDatasResponse<object> pageDatasResponse = new PageDatasResponse<object>();
                pageDatasResponse.code = this.code;
                pageDatasResponse.data = this.data;
                pageDatasResponse.message = this.message;
                pageDatasResponse.page = new Pagination(this.total, this.currPage, this.pageSize, this.totalPage);
                pageDatasResponse.extend = this.extend;
                baseResponse = pageDatasResponse;
            }
            else if (this.data != null)
            {
                DataResponse<object> datasResponse = new DataResponse<object>();
                datasResponse.code=this.code;
                datasResponse.data=this.data;
                datasResponse.message=this.message;
                datasResponse.extend=this.extend;
                baseResponse = datasResponse;
            }
            else
            {
                baseResponse = new BaseResponse();
                ((BaseResponse)baseResponse).message=this.message;
                ((BaseResponse)baseResponse).code=this.code;
                ((BaseResponse)baseResponse).extend=this.extend;
            }
            return (BaseResponse)baseResponse;
        }

        

        public ResponseBuilder Extend(Dictionary<String, object> extend)
        {
            this.extend = extend;
            return this;
        }
        public ResponseBuilder Token(AzureTableToken token)
        {
            this.token = token;
            return this;
        }
        public ResponseBuilder Data(object data)
        {
            this.data = data;
            return this;
        }
       
        public ResponseBuilder Page(Pagination page)
        {
            this.pageSize = page.pageSize;
            this.currPage = page.currPage;
            this.total = page.total;
            this.page = page;
            this.totalPage = (int)Math.Ceiling((double)this.total / (double)this.pageSize);
            return this;
        }
        public ResponseBuilder totalCount(int totalCount)
        {
            this.total = totalCount;
            return this;
        }

        public ResponseBuilder CurrPage(int currPage)
        {
            this.currPage = currPage;
            return this;
        }

        public ResponseBuilder PageSize(int pageSize)
        {
            this.pageSize = pageSize;
            return this;
        }

        public ResponseBuilder TotalPage(int totalPage)
        {
            this.totalPage = totalPage;
            return this;
        }

        public ResponseBuilder success()
        {
            return this;
        }

        public ResponseBuilder success(String message)
        {
            this.message = message;
            return this;
        }

        public ResponseBuilder success(String message, int code)
        {
            this.message = message;
            this.code = code;
            return this;
        }

        public ResponseBuilder failed(String message, int code)
        {
            this.code = code;
            this.message = message;
            return this;
        }

        public ResponseBuilder failed(String message)
        {
            this.code = 1;
            this.message = message;
            return this;
        }
    }
}
