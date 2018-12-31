using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using FluentAssertions;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Client;
using Xunit;

namespace JsonRpc.Tests
{
    public class OutputHandlerTests
    {
        private static (OutputHandler handler, Func<Task> wait) NewHandler(Stream Writer,Action<CancellationTokenSource> action)
        {
            var cts = new CancellationTokenSource();
            if (!System.Diagnostics.Debugger.IsAttached)
                cts.CancelAfter(TimeSpan.FromSeconds(120));
            action(cts);

            var handler = new OutputHandler(
                Writer,
                new Serializer());
            handler.Start();
            return (handler, () => {
                cts.Wait();
                return Task.Delay(50);
            });
        }

        [Fact]
        public async Task ShouldSerializeValues()
        {
            var w = Substitute.For<Stream>();
            var received = "";
            w.CanWrite.Returns(true);

            var (handler, wait) = NewHandler(w, cts => {
                w.When(x => x.Write(Arg.Any<byte[]>(), Arg.Any<int>(), Arg.Any<int>()))
                    .Do(c => {
                        received = System.Text.Encoding.UTF8.GetString(c.ArgAt<byte[]>(0), 0, c.ArgAt<int>(2));
                        cts.Cancel();
                    });
            });
            var value = new Response(1, 1);

            using (handler)
            {

                handler.Send(value);
                await wait();
                const string send = "Content-Length: 35\r\n\r\n{\"jsonrpc\":\"2.0\",\"id\":1,\"result\":1}";
                received.Should().Be(send);
                var b = System.Text.Encoding.UTF8.GetBytes(send);
                w.Received().Write(Arg.Any<byte[]>(), 0, b.Length); // can't compare b here, because it is only value-equal and this test tests reference equality
            }
        }
    }
}
