using System.Collections.Generic;

namespace TEAMModelOS.SDK.Extension.DataResult.PageToken
{
    public class PaginationData<T>:Pagination
    {
        public PaginationData(int currPage, int pageSize, int total) : base(currPage, pageSize, total)
        {
        }
        public List<T> data { get; set; }
    }
}
