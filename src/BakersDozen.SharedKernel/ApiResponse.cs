using System;
using System.Collections.Generic;
using System.Text;

namespace BakersDozen.SharedKernel
{
    public class ApiResponse<T>
    {
	    public ApiResponse(
		    T values,
		    string message = "")
	    {
		    this.Values = values;
		    this.Message = message;
	    }

        public T Values { get; set; }

        public string Message { get; set; }
    }
}
