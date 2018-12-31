using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Serialization;
using Xunit;

namespace Lsp.Tests.Models
{
    public class WorkspaceEditTests
    {
        [Theory, JsonFixture]
        public void SimpleTest(string expected)
        {
            var model = new WorkspaceEdit()
            {
                Changes = new Dictionary<Uri, IEnumerable<TextEdit>>() {
                        {
                            new Uri("file:///abc/123/d.cs"), new [] {
                                new TextEdit() {
                                    NewText = "new text",
                                    Range = new Range(new Position(1, 1), new Position(2,2))
                                },
                                new TextEdit() {
                                    NewText = "new text2",
                                    Range = new Range(new Position(3, 3), new Position(4,4))
                                }
                            }
                        }
                    }
            };
            var result = Fixture.SerializeObject(model);

            result.Should().Be(expected);

            var deresult = new Serializer(ClientVersion.Lsp3).DeserializeObject<WorkspaceEdit>(expected);
            deresult.Should().BeEquivalentTo(model);
        }

        [Theory, JsonFixture]
        public void DocumentChangesTest(string expected)
        {
            var model = new WorkspaceEdit()
            {
                DocumentChanges = new Container<TextDocumentEdit>(
                        new TextDocumentEdit()
                        {
                            TextDocument = new VersionedTextDocumentIdentifier()
                            {
                                Version = 1,
                                Uri = new Uri("file:///abc/123/d.cs"),
                            },
                            Edits = new[] {
                                new TextEdit() {
                                    NewText = "new text",
                                    Range = new Range(new Position(1, 1), new Position(2,2))
                                },
                                new TextEdit() {
                                    NewText = "new text2",
                                    Range = new Range(new Position(3, 3), new Position(4,4))
                                }
                            }
                        },
                        new TextDocumentEdit()
                        {
                            TextDocument = new VersionedTextDocumentIdentifier()
                            {
                                Version = 1,
                                Uri = new Uri("file:///abc/123/b.cs"),
                            },
                            Edits = new[] {
                                new TextEdit() {
                                    NewText = "new text2",
                                    Range = new Range(new Position(1, 1), new Position(2,2))
                                },
                                new TextEdit() {
                                    NewText = "new text3",
                                    Range = new Range(new Position(3, 3), new Position(4,4))
                                }
                            }
                        }
                    )
            };
            var result = Fixture.SerializeObject(model);

            result.Should().Be(expected);

            var deresult = new Serializer(ClientVersion.Lsp3).DeserializeObject<WorkspaceEdit>(expected);
            deresult.Should().BeEquivalentTo(model);
        }
    }
}
