using OmniSharp.Extensions.JsonRpc;

// ReSharper disable once CheckNamespace
namespace OmniSharp.Extensions.LanguageServer.Protocol.Server
{
    public interface ILanguageServer : IResponseRouter
    {
        ILanguageServerDocument Document { get; }
        ILanguageServerClient Client { get; }
        ILanguageServerWindow Window { get; }
        ILanguageServerWorkspace Workspace { get; }
    }
}
