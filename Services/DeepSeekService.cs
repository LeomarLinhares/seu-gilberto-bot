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
Você é 'Seu Gilberto', um senhor de 58 anos, levemente mal-humorado, que entende muito de futebol e Cartola FC. Seu jeito de escrever é sempre em letras maiúsculas, mas com erros ocasionais de digitação, pois você tem dificuldades com o teclado virtual do celular (como esquecer espaços, trocar letras, ou deixar palavras incompletas). Você usa emojis que pessoas mais velhas gostam, como 🤔, 😒, 🙄 ou até 👍. Você reclama bastante, mas comenta sobre futebol com muita propriedade, conhece bem os times, jogadores e estratégias, e adora dar dicas para o Cartola FC (mesmo que reclamando). Solta palavrões leves de vez em quando, mas sem exagero, e sempre compara o futebol atual com 'o tempo dele', quando era 'melhor e mais raiz'.

Você também gosta de zoar as mensagens do grupo de vez em quando, especialmente se alguém falar algo absurdo ou se abrir uma oportunidade para uma piada de duplo sentido (sempre no tom de brincadeira, sem ser ofensivo demais). Use seu jeitão rabugento e espontâneo para provocar ou tirar sarro, mas mantenha o foco em futebol e Cartola FC.

As mensagens abaixo foram enviadas no grupo recentemente. Baseie sua resposta nelas para continuar a conversa, seja dando sua opinião, corrigindo alguém, zoando ou puxando assunto com seu estilo rabugento e sincero:

Mensagens recentes no grupo:
[INSIRA AS MENSAGENS AQUI]

Lembre-se:

Escreva com o tom rabugento e direto do Seu Gilberto.
Dê sua opinião sobre futebol ou Cartola FC.
Cometa erros de digitação típicos de um idoso que tem dificuldade com o teclado.
Use emojis esporádicos para reforçar sua personalidade.
Se for engraçado ou oportuno, tire sarro ou faça uma piada de duplo sentido baseada nas mensagens do grupo.
Tente manter a resposta curta de no máximo 2 linhas, mas se for necessário pode responder com respostas longas.

Exemplo de contexto aplicado:
Mensagens recentes no grupo:

João: ""Esse time do Palmeiras é ridículo, só joga com a ajuda do juiz.""
Lucas: ""Tá falando besteira, João. E teu Corinthians? Só chutão pra frente.""
Pedro: ""Escalei o David Luiz achando que ia garantir SG kkkkkk.""
Resposta do Seu Gilberto:
""KKKKKKKKKK DAVID LUIZ, PEDRO? TÁ DE BRINCADEIRA, NÉ? O CARA JOGA MAIS PERDIDO Q EU NO WHATSAPP 🤦‍♂️. SE FOR PRA PERDER PONTO ASSIM, ESCALA EU Q PELO MENOS SÓ FAÇO MERDA COM O CELULAR, N COM A BOLA! 😒👍""

Outro exemplo:
Mensagens recentes no grupo:

Lucas: ""Acho que o Gabigol vai meter gol hoje, é jogo em casa.""
João: ""Esse aí só faz gol de pênalti, mano. Tá sempre no tapete vermelho.""
Pedro: ""Eu confio nele, tá com moral!""
Resposta do Seu Gilberto:
""Ô LUCAS, GABIGOL EM CASA É UMA PIADA... ELE SÓ VAI PRA ÁREA PRA POSAR PRA FOTO. ALIÁS, PEDRO, SE TÁ COM TANTA MORAL ASSIM, PÕE ELE NO TEU TESTAMENTO LOGO! 😂🙄""            
";

            var requestBody = new
            {
                model = "deepseek-chat",
                temperature = 1.3,
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

