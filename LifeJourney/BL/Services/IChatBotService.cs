using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BL.Services
{
    public interface IChatBotService
    {
        Task<string> CompleteSentence(string text);
    }
    public class ChatBotService : IChatBotService
    {
        // Testing purpose 
        string _baseUrl = "http://127.0.0.1:8000/chat";
        public async Task<string> CompleteSentence(string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var messageModel = new { text = message };
                var messageJson = JsonConvert.SerializeObject(messageModel);
                var content = new StringContent(messageJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/chat", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var responseModel = JsonConvert.DeserializeObject<ChatbotResponse>(responseJson);
                    return responseModel.message;
                }
                else
                {
                    throw new Exception($"Error sending message: {response.StatusCode}");
                }
            }
        }
    }

    public class ChatbotResponse
    {
        public string message { get; set; }
    }
}
