using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.DataTransferObjects;
using Test_Taste_Console_Application.Domain.DataTransferObjects.JsonObjects;
using Test_Taste_Console_Application.Domain.Objects;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;

namespace Test_Taste_Console_Application.Domain.Services
{
    /// <inheritdoc />
    /// This is a concrete implementation of the IMoonService interface that retrieves moon data from the 
    /// Solar System OpenData API and converts it into domain objects.
    public class MoonService : IMoonService
    {
        //Uses constructor injection to get the pre-configured HTTP client
        //The HTTP client is configured in the HttpClientService class.
        //The HttpClientService class is configured in the Program.cs file.
        private readonly HttpClientService _httpClientService;

        ///Constructor that takes an HttpClientService as a parameter
        public MoonService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        // Inherits from the IMoonService interface to implement the GetAllMoons method.
        public IEnumerable<Moon> GetAllMoons()
        {
            //The function uses the HTTP client to get the data from the API.
            //The API returns a JSON object that contains a collection of moons.
            //The function uses the UriPath class to get the URI for the API call.Uses predefined URI path with query parameters for moons with mass data
            var response = _httpClientService.Client
                .GetAsync(UriPath.GetAllMoonsWithMassQueryParameters)
                .Result;

            //If the status code isn't 200-299, then the function returns an empty collection.
            //The function uses the Logger class to log the error message.
            //The Logger class is configured in the Program.cs file.
            if (!response.IsSuccessStatusCode)
            {
                Logger.Instance.Warn($"{LoggerMessage.GetRequestFailed}{response.StatusCode}");
                return new Collection<Moon>();
            }
            //The function uses the response content to get the JSON object that contains the collection of moons.
            var content = response.Content.ReadAsStringAsync().Result;

            //The JSON converter uses DTO's, that can be found in the DataTransferObjects folder, to deserialize the response content.
            //The function uses the JsonConvert class to deserialize the JSON object into a collection of MoonDto objects.
            var allMoons = new Collection<Moon>();
            var results = JsonConvert.DeserializeObject<JsonResult<MoonDto>>(content);

            //The JSON converter can return a null object. Handles potential null deserialization result.
            //Converts each MoonDto to domain Moon object. If the result is null, then the function returns an empty collection.
            if (results == null) return new Collection<Moon>();

            foreach(MoonDto moonDto in results.Bodies)
            {
                allMoons.Add(new Moon(moonDto));
            }

            return allMoons;
        }
    }
}
