using MessagePack;
using System;
using System.Collections.Generic;

namespace TEAMModelOS.SDK.Extension.DataResult.PageToken
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class Pagination
    {
        public long total { get; set; }
        public int currPage { get; set; }
        public int pageSize { get; set; } 
        public int totalPage { get; set; }
      
        public Pagination() { }
        public Pagination(long total, int currPage, int pageSize)
        {
            this.total = total;
            this.currPage = currPage;
            this.pageSize = pageSize;
            this.totalPage = (int)Math.Ceiling((double)this.total / (double)this.pageSize);
        }
        public Pagination(long total, int currPage, int pageSize, int totalPage)
        {
            this.total = total;
            this.currPage = currPage;
            this.pageSize = pageSize;
            this.totalPage = totalPage;
        }
    }
}
