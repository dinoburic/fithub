using FitHub.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitHub.Infrastructure.Common
{
    public class CaptchaService : ICaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CaptchaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> VerifyAsync(string token)
        {
            var captchaEnabled = _configuration.GetValue<bool>("Captcha:Enabled");
            if (!captchaEnabled)
            {
                return true;
            }

            if (string.IsNullOrEmpty(token)) return false; 

            var secretKey = _configuration["Captcha:SecretKey"];

            var response = await _httpClient.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}",
                null);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<RecaptchaResponse>(json, options);

            return result?.Success == true; 
        }

        private class RecaptchaResponse
        {
            public bool Success { get; set; }
        }
    }
}
