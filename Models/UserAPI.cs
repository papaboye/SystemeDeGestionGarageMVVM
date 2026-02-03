using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Security.RightsManagement;
using Newtonsoft.Json;

namespace TravailPratique2.Models
{
    class UserAPI
    {
        private HttpClient _users;
        public UserAPI()
        {
            _users = new HttpClient();
            _users.BaseAddress = new Uri("https://dummyjson.com/");
            _users.DefaultRequestHeaders.Accept.Clear();
            _users.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Models.Utilisateur>> GetUtilisateursAsync()
        {
            List<Utilisateur> utilisateurs = new List<Utilisateur>();
            HttpResponseMessage response = await _users.GetAsync("users");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<UtilisateurReponse>(data);
                utilisateurs = userResponse.users;

            }    
                return utilisateurs;
        }
        
    }
}
