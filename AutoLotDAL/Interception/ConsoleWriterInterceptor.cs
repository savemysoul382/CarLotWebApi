using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AutoLotDAL.Interception
{
    public class ConsoleWriterInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<Int32> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<Int32> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<Object> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<Object> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        private void WriteInfo(bool isAsync, string CommandText)
        {
            WriteLine($"IsAsync: {isAsync}, CommandText: {CommandText}");
        }
    }
}