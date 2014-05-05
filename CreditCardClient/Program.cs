using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using CreditCard.Models;

namespace CreditCardClient
{
    class Program
    {
        // get cards details
        static async Task GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://x00077517.cloudapp.net/");                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET ../api/stock
                    // get all stock listings
                    HttpResponseMessage response = await client.GetAsync("api/CreditCard");                  // async call, await suspends until result available            
                    if (response.IsSuccessStatusCode)                                                   // 200.299
                    {
                        // read result 
                        var cards = await response.Content.ReadAsAsync<IEnumerable<CreditCardItem>>();                   
                        foreach (var card in cards)
                        {
                            Console.WriteLine("\tId: " + card.Id + ", number: " + card.Number + ", risk level " + card.RiskLevel);
                        }                        
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    Console.Write("\n\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // add a stock listing
        static async Task AddAsync(string cnumber, int rlevel)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://x00077517.cloudapp.net/");                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON - preference for response 
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // POST /api/CreditCard with a listing serialised in request body
                    // create a new credit card
                    CreditCardItem newCard = new CreditCardItem { Number = cnumber, RiskLevel = rlevel };
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/CreditCard", newCard);   // or PostAsXmlAsync
                    if (response.IsSuccessStatusCode)                                                       // 200 .. 299
                    {
                        Uri newCardUri = response.Headers.Location;
                        var card = await response.Content.ReadAsAsync<CreditCardItem>();
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // update a stock listing
        static async Task UpdateAsync(string id, string cnumber_new, int rlevel_new)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://x00077517.cloudapp.net/");

                    CreditCardItem card = new CreditCardItem() { Number = cnumber_new, RiskLevel = rlevel_new };                                            

                    // update by Put to /api/CreditCard/{id} a listing serialised in request body
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/CreditCard/" + id, card);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // delete credit card
        static async Task DeleteAsync(string id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://x00077517.cloudapp.net/");
                                          
                    HttpResponseMessage response = await client.DeleteAsync("api/CreditCard/" + id);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main(string[] args)
        {
            GetAllAsync().Wait();

            AddAsync("123", 2).Wait();
            GetAllAsync().Wait();
            
           //UpdateAsync("5364ec0c07fc29103ce97387", "7777779", 3).Wait();
          //GetAllAsync().Wait();

           //DeleteAsync("5364ec0c07fc29103ce97387").Wait();
           //GetAllAsync().Wait();
        }
    }
}
