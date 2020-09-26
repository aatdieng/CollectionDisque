using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exercice.Controllers
{
    [ApiController]
    public class CollectionMusiqueController : ControllerBase
    {
        
        private static IConfiguration _configuration;

        private static  string _apiBaseUrl;

        public CollectionMusiqueController(IConfiguration configuration)
        {
            _configuration = configuration;

            _apiBaseUrl = _configuration.GetValue<string>("WebAPIBaseUrl");
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{page?}")]
        public async Task<ObjetRetourAPi>  GetCollectionMusiqueParPage(int? page)
        {
            if (page > 1)
            {
                _apiBaseUrl = _apiBaseUrl.Remove(_apiBaseUrl.Length - 1, 1) + page.ToString();
            }

            return await AppelerWebApiExterne();
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ObjetRetourAPi> GetFirstPageCollectionMusique()
        {
            return await AppelerWebApiExterne();
        }


        private static async Task<ObjetRetourAPi> AppelerWebApiExterne()
        {
            ObjetRetourAPi objetRetourApi = new ObjetRetourAPi();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                //HTTP GET
                client.DefaultRequestHeaders.TryAddWithoutValidation("access-control-allow-headers", "Content-Type, authorization, User-Agent, Private-Auth-Secret, Discogs-UID");
                client.DefaultRequestHeaders.TryAddWithoutValidation("access-control-allow-origin", "*");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/vnd.discogs.v2.html+json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("content-type", "application/json");
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("ausamerika");

                var responseTask = await client.GetAsync(new Uri(_apiBaseUrl));
                if (responseTask.IsSuccessStatusCode)
                {
                    objetRetourApi = await responseTask.Content.ReadAsAsync<ObjetRetourAPi>();
                    objetRetourApi.NombreTotalArticles = objetRetourApi.releases.Count();

                }
            }

            return objetRetourApi;
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{page?}/{nombreAretourner}")]
        public async Task<IEnumerable<CollectionMusique>> GetFiveRecordRandomly(int? page, int nombreAretourner)
        {
            if (page > 0)
            {
                _apiBaseUrl = _apiBaseUrl.Remove(_apiBaseUrl.Length - 1, 1) + page.ToString();
            }

            var collectionDisque =  await AppelerWebApiExterne();
            CollectionMusique[] listeCollection = collectionDisque.releases.ToArray();

            var rng = new Random();
            return Enumerable.Range(1, nombreAretourner).Select(index => new CollectionMusique
            {
                id = listeCollection[rng.Next(listeCollection.Length)].id,
                instance_id = listeCollection[rng.Next(listeCollection.Length)].instance_id,
                date_added = listeCollection[rng.Next(listeCollection.Length)].date_added,
                rating = listeCollection[rng.Next(listeCollection.Length)].rating,
                basic_information = listeCollection[rng.Next(listeCollection.Length)].basic_information


            });
        }

    }
}
