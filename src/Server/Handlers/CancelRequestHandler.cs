using System.Threading;
using System.Threading.Tasks;
using OmniSharp.Extensions.Embedded.MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace OmniSharp.Extensions.LanguageServer.Server.Handlers
{
    public class CancelRequestHandler : ICancelRequestHandler
    {
        private readonly ILspRequestRouter _requestRouter;

        internal CancelRequestHandler(ILspRequestRouter requestRouter)
        {
            _requestRouter = requestRouter;
        }

        public Task<Unit> Handle(CancelParams notification, CancellationToken token)
        {
            _requestRouter.CancelRequest(notification.Id);
            return Unit.Task;
        }
    }
}
