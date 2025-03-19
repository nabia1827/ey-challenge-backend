using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Serilog;

namespace Challenge.Services.WebApi.Modules.Logger
{
    public static class LogHelper
    {
        public static string RequestPayload = "";

        public static async void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            var path = request.Path;

            LogUserInfoFromToken(httpContext);


            string responseBodyPayload = await ReadResponseBody(httpContext.Response);

            //Show complete request & response if it's not the authenticate endpoint
            if (path != "/api/Auth/Authenticate")
            {
                Log.Information($"Request Body: {RequestPayload}");
                Log.Information($"Response Body: {responseBodyPayload}");
            }
            //Limit the response body to show only relevant data and hide private information 
            else
            {
                var jsonObject = JsonConvert.DeserializeObject<JObject>(responseBodyPayload);
                if (jsonObject?["data"] is JObject dataObject)
                {
                    int userId = dataObject?["data"]?["UserId"]?.Value<int>() ?? 0;
                    string username = dataObject?["data"]?["Username"]?.Value<string>() ?? "";
                    int profileId = dataObject?["data"]?["ProfileId"]?.Value<int>() ?? 0;

                    // Log relevant data
                    Log.Information("Response Body: userId: {userId}, username: {username}, profileId:{profileId}", userId, username, profileId);
                }
                else
                {
                    // Log complete response body if data is not present
                    Log.Information($"Response Body: {responseBodyPayload}");
                }
            }


            // Set all the common properties available for every request
            Log.Information("Host: {Host}, Protocol: {Protocol}, Scheme: {Scheme}", request.Host, request.Protocol, request.Scheme);

            // Only set it if available. You're not sending sensitive data in a querystring right?!
            if (request.QueryString.HasValue)
            {
                Log.Information("QueryString: {QueryString}", request.QueryString.Value);
            }

            // Set the content-type of the Response at this point
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

            // Retrieve the IEndpointFeature selected for the request
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object) // endpoint != null
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{responseBody}";
        }

        private static void LogUserInfoFromToken(HttpContext context)
        {
            var user = context.User;

            // Check if the user is authenticated
            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user?.FindFirst("UserId")?.Value;
                var userName = user?.FindFirst("Username")?.Value;
                var perfilId = user?.FindFirst("ProfileId")?.Value;
                // Log user information
                Log.Information($"User: {userName}, ID: {userId}, Profile: {perfilId}");
            }
        }
    }
}
