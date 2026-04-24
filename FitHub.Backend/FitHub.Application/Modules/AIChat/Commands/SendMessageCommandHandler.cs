using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.AIChat.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, SendMessageResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public SendMessageCommandHandler(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<SendMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var apiKey = _config["GroqConfig:ApiKey"];
            var model = _config["GroqConfig:Model"] ?? "llama3-8b-8192";
            var groqUrl = "https://api.groq.com/openai/v1/chat/completions";

            var messagesList = new List<object>
            {
                // Uvijek prvo ide sistemski prompt
                new { role = "system", content = "You are an AI assistant for FitHub, online platform for buying fitness plans. Please respond shortly in English" }
            };

            // Ako postoji historija sa frontenda, dodajemo je u niz
            if (request.History != null && request.History.Any())
            {
                foreach (var msg in request.History)
                {
                    messagesList.Add(new { role = msg.Role, content = msg.Content });
                }
            }

            messagesList.Add(new { role = "user", content = request.Message });

            var payload = new
            {
                model = model,
                messages = messagesList
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // 2. CREATE HttpRequestMessage (resolving item 21 - thread safety)
            // Umjesto mutiranja _httpClient.DefaultRequestHeaders, kreiramo izolovan request
            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, groqUrl)
            {
                Content = httpContent
            };
            
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // Send built request through shared HTTP client
            var response = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            using var jsonDoc = JsonDocument.Parse(responseString);

            var replyText = jsonDoc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return new SendMessageResponse { Reply = replyText };
        }
    }
    }
