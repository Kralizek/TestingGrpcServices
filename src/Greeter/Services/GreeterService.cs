using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Greeter
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var response = new HelloReply
            {
                Messages =
                {
                    request.Languages
                           .Where(language => Formats.ContainsKey(language))
                           .Select(language => new { language, format = Formats[language] })
                           .ToDictionary(k => k.language.ToString(), v => string.Format(v.format, request.Name))
                }
            };

            return Task.FromResult(response);
        }

        private static readonly IReadOnlyDictionary<Language, string> Formats = new Dictionary<Language, string>
        {
            [Language.English] = "Hello, {0}",
            [Language.French] = "Salut, {0}",
            [Language.Italian] = "Ciao, {0}",
            [Language.Spanish] = "Hola, {0}",
            [Language.Swedish] = "Hej, {0}"
        };
    }
}
