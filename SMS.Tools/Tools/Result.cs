using Microsoft.AspNetCore.Mvc;
using SMS.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Tools.Tools
{
    public class Result
    {
        public ResultState State { get; }
        public string? Detail { get; }
        public Exception? Exception { get; set; }

        public static Result Success
            => In(ResultState.Success, "Operation successful.");
        public static Result ParamEntityNullFail
            => In(ResultState.ParamEntityNullFail, "Parameter entity is null for operation.");
        public static Result ContextInnerFail
            => In(ResultState.ContextInnerFail, "Internal failure of database context during operation.");
        public static Result NoMatchedByIdFail
            => In(ResultState.NoMatchedByIdFail, "No entity instance matched by the given ID value during operation.");
        public static Result Fail
            => In(ResultState.Fail, "Undefined failure during operation.");

        private Result(ResultState finishState, string detail, Exception? ex = null)
        {
            State = finishState; Detail = detail; Exception = ex;
        }

        public static Result In(ResultState finishState, string detail)
        {
            return new Result(finishState, detail);
        }

        public static Result In(ResultState finishState, string detail, Exception ex)
        {
            return new Result(finishState, detail, ex);
        }

        public static Result In(Exception ex)
        {
            if (ex is ArgumentNullException)
                return ParamEntityNullFail;

            else if (ex is DbException)
                return ContextInnerFail;

            else if (ex is ArgumentException)
                return NoMatchedByIdFail;

            else
                return Fail;
        }
    }
}
