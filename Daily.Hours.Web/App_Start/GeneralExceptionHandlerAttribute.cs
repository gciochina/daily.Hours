using log4net.Core;
using log4net.Repository.Hierarchy;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace Daily.Hours.Web
{
    public class ExceptionLoggerFilter : IExceptionFilter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ExceptionLoggerFilter()
        {
        }

        public bool AllowMultiple { get { return true; } }

        public Task ExecuteExceptionFilterAsync(
                HttpActionExecutedContext actionExecutedContext,
                CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                log.Error($"ERROR: {actionExecutedContext.Exception.Message}", actionExecutedContext.Exception);
            }, cancellationToken);
        }
    }
}