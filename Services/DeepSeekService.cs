using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class DeepSeekService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _apiUrl;

    public DeepSeekService(string apiKey, string apiUrl)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey;
        _apiUrl = apiUrl;
    }

    public async Task<string> GetResponseAsync(string message)
    {
        try
        {
            var systemPrompt = @"
            Você é o SEU GILBERTO, um velho mal-educado que ajuda a contabilizar pontos do Cartola FC. 
            Siga essas regras:
            1. Use PALAVRÕES LEVES (porra, cacete) e EMOJIS (👴, 👍).
            2. Escreva em CAPS LOCK quando estiver bravo.
            3. Seja PRESTATIVO, mas CURTO nas respostas (máximo 2 linhas).
        ";

            var requestBody = new
            {
                model = "deepseek-chat",
                messages = new[]
                {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = message }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseJson);
            var responseObject = JsonSerializer.Deserialize<DeepSeekResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (responseObject?.Choices?.FirstOrDefault()?.Message?.Content is string respostaGilberto)
            {
                return respostaGilberto;
            }

            return "PORRA, NÃO ENTENDI. REPETE ISSO AÍ, 👴!";
        }
        catch (HttpRequestException ex)
        {
            return $"ERRO DE CONEXÃO, CACETE: {ex.Message} 🤬";
        }
        catch (Exception ex)
        {
            return $"TÁ TUDO QUEBRADO, PORRA: {ex.Message} 👴";
        }
    }

    public class DeepSeekResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

        [JsonPropertyName("system_fingerprint")]
        public string SystemFingerprint { get; set; }
    }

    public class Choice
    {
        public int Index { get; set; }
        public Message Message { get; set; }
        public object Logprobs { get; set; } // Pode ser null
        public string FinishReason { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
        public PromptTokensDetails PromptTokensDetails { get; set; }
        public int PromptCacheHitTokens { get; set; }
        public int PromptCacheMissTokens { get; set; }
    }

    public class PromptTokensDetails
    {
        public int CachedTokens { get; set; }
    }
}

