using Jeppes.GatewayApi.Exceptions;
using Jeppes.GatewayApi.Helpers;
using Jeppes.GatewayApi.JsonConverters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jeppes.GatewayApi
{
    public class ApiHandler
    {
        private readonly ApiSettings _settings;

        public ApiHandler(ApiSettings settings)
        {
            _settings = settings;
        }

        public static MessageStatus DeserializeMessageStatus(string json)
        {
            return Deserializer.DeserializeMessageStatus(json);
        }

        /// <summary>
        /// Sends a single message to the API.
        /// </summary>
        /// <param name="message">The message which should be send through the API.</param>
        /// <exception cref="ApiException">Thrown when the API is not able to give a 200 status code.</exception>
        /// <returns></returns>
        public async Task<MessagesSentResponse<MessageResponse>> SendMessageAsync(Message message)
        {
            var multipleResponse = await SendMessagesAsync(message);
            return new MessagesSentResponse<MessageResponse>(multipleResponse.UsageDetails, multipleResponse.MessageResponseDetails[0]);
        }

        /// <summary>
        /// Sends multiple messages to the API in the same request.
        /// </summary>
        /// <param name="messages">The messages which should be send through the API.</param>
        /// <exception cref="ApiException">Thrown when the API is not able to give a 200 status code.</exception>
        /// <returns></returns>
        public async Task<MessagesSentResponse<IImmutableList<MessageResponse>>> SendMessagesAsync(params Message[] messages)
        {
            var request = new RestRequest("mtsms", Method.POST);
            request.AddJsonBody(messages.ToJson());

            var content = await ExecuteAsync(request);

            using var jsonContent = JsonDocument.Parse(content);

            var usageJson = jsonContent.RootElement.GetProperty("usage").ToString();
            var usage = Deserializer.DeserializeUsageDetails(usageJson);

            var messageIds = jsonContent.RootElement.GetProperty("ids").EnumerateArray().Select(element => element.GetInt64()).ToImmutableArray();
            if (messageIds.Length != messages.Length)
                throw new ApiException(0, "The number of message IDs returned from the API is not the same as the number of messages sent to the API.");

            var messageResponses = messageIds.Select((messageId, index) =>
            {
                var message = messages[index];
                return new MessageResponse(messageId, message);
            }).ToImmutableList();

            return new MessagesSentResponse<IImmutableList<MessageResponse>>(usage, messageResponses);
        }

        private async Task<string> ExecuteAsync(IRestRequest request)
        {
            var restClient = CreateRestClient();

            var response = await restClient.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            else if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var errorDetails = string.IsNullOrWhiteSpace(response.Content) ? string.Empty : response.Content + Environment.NewLine;
                if (!string.IsNullOrWhiteSpace(response.ErrorMessage))
                    errorDetails += $"Error message: {response.ErrorMessage}";

                throw new ApiException((int)response.StatusCode, errorDetails, response.ErrorException);
            }
            else
            {
                var errorDetails = string.IsNullOrWhiteSpace(response.Content) ? string.Empty : response.Content + Environment.NewLine;
                if (!string.IsNullOrWhiteSpace(response.ErrorMessage))
                    errorDetails += $"Error message: {response.ErrorMessage}";

                throw new ApiException((int)response.StatusCode, errorDetails, response.ErrorException);
            }
        }

        private RestClient CreateRestClient()
        {
            var restClient = new RestClient(_settings.Url);
            restClient.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(_settings.ApiToken, "");
            return restClient;
        }
    }
}
