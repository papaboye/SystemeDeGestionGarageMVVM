using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace TravailPratique2.Models;

internal sealed class UserAPI
{
    private static readonly HttpClient Client = CreateClient();

    public async Task<IReadOnlyList<Utilisateur>> GetUtilisateursAsync()
    {
        using var response = await Client.GetAsync("users");
        response.EnsureSuccessStatusCode();

        var contenu = await response.Content.ReadAsStringAsync();
        var resultat = JsonConvert.DeserializeObject<UtilisateurReponse>(contenu);
        return resultat?.users ?? [];
    }

    private static HttpClient CreateClient()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("https://dummyjson.com/"),
            Timeout = TimeSpan.FromSeconds(15)
        };
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        return client;
    }
}
