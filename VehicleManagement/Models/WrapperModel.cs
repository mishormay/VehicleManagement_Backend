using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class WrapperModel
    {
    }

    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }

    public class PagedResponse<T> : Response<T>
    {
        public int currentPage { get; set; }
        public int perPage { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.currentPage = pageNumber;
            this.perPage = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }

    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int perPage { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.perPage = 5;
        }
        public PaginationFilter(int pageNumber)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            //this.perPage = pageSize > 10 ? 10 : pageSize;
            this.perPage = 5;
        }
    }

}
