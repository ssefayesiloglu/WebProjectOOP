using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebProjectOOP.Business.Abstract;
using WebProjectOOP.Entities.Dtos;

namespace WebProjectOOP.Business.Concrete
{
    public class OllamaAiService : IAiService
    {
        private readonly HttpClient _httpClient;

        public OllamaAiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<string> GenerateDescriptionAsync(string alldata)
        {
            // Yeni Kurumsal Prompt Düzenlemesi
            var requestBody = new OllamaRequest
            {
                model = "llama3.2",
                prompt = $@"Sen resmi bir proje analiz asistanısın. 
    Aşağıdaki verileri kullanarak sadece 5 cümlelik profesyonel bir Türkçe rapor yaz. 
    İNGİLİZCE VEYA İSPANYOLCA KELİME KULLANMA. 
    
    Veri kümesi: {alldata}
    
    Rapor Formatı:
    1. Mevcut durum özeti (2 cümle).
    2. Stratejik gelişim önerisi (2 cümle).
    3. Genel sonuç (1 cümle).",
                options = new OllamaOptions { temperature = 0 } // Modeli ciddiyete davet ediyoruz
            };

            var response = await _httpClient.PostAsJsonAsync("http://localhost:11434/api/generate", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OllamaResponse>();
                return result?.response ?? "Analiz raporu oluşturulamadı.";
            }

            return "AI Servisine ulaşılamadı. Lütfen Ollama servisinin açık olduğundan emin olun.";
        }
    }
}