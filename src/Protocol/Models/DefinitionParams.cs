using OmniSharp.Extensions.Embedded.MediatR;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Models
{
    public class DefinitionParams : TextDocumentPositionParams, IRequest<LocationOrLocations>
    {

    }
}