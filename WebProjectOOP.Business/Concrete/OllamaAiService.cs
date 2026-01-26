using System;
using System.Collections.Generic;
/*using System.Net.Http.Json;
using WebProjectOOP.Business.Abstract;
using WebProjectOOP.Entities.Dtos;
using Microsoft.Identity.Client;

namespace WebProjectOOP.Business.Concrete
{
    public class OllamaAiService : IAiService
    {
        private readonly HttpClient _httpClient;

        public OllamaAiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateDescriptionAsync(string title);

        {
            var requestBody = new OllamaRequest
            {
                Prompt
            }








        }






}*/
//İleride Ollama yerine ChatGPT kullanmak istersek, sadece yeni bir sınıf yazıp bu interface'den türetmemiz yeterli olacaktır. Kodumuz esnek kalır.

using System.Net.Http.Json;
using System.Text.Json;
using WebProjectOOP.Business.Abstract;
using WebProjectOOP.Entities.Dtos;

namespace WebProjectOOP.Business.Concrete
{
    public class OllamaAiService : IAiService
    {
        private readonly HttpClient _httpClient;

       
        public OllamaAiService(HttpClient httpClient)   // Dependency Injection ile HttpClient alıyoruz (Haberleşme hattımız)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateDescriptionAsync(string title)
        {
            
            var requestBody = new OllamaRequest   // 1. AI'ya ne soracağımızı hazırlıyoruz
            {
                prompt = $"Bana '{title}' görevi için 2 cümlelik kısa ve motive edici 'türkçe' bir açıklama yazar mısın?"
            };

            
            var response = await _httpClient.PostAsJsonAsync("http://localhost:11434/api/generate", requestBody);

            if (response.IsSuccessStatusCode)
            {
               
                var result = await response.Content.ReadFromJsonAsync<OllamaResponse>();
                return result?.response ?? "Açıklama üretilemedi.";   // 3. Gelen JSON cevabını okuyoruz
            }

            return "AI Servisine ulaşılamadı.";
        }
    }
}