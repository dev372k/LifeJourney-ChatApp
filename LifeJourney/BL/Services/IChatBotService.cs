using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BL.Services
{
    public interface IChatBotService
    {
        Task<string> CompleteSentence(string _baseUrl, string text);
    }
    public class ChatBotService : IChatBotService
    {
        // Testing purpose 
        public async Task<string> CompleteSentence(string _baseUrl, string message)
        {
            try
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
                        return "Please wait, will get back to you :)";
                    }
                }
            }
            catch (Exception)
            {
                return "Please wait, will get back to you :)";
            }
        }
    }

    public class ChatbotResponse
    {
        public string message { get; set; }
    }
}
