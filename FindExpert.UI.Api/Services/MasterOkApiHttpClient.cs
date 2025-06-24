using System.Net.Http.Json;
using System.Text.Json;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Api.Models.Advertisement.Create;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Api.Models.Advertisement.GetList;
using FindExpert.UI.Api.Models.Advertisement.GetMyList;
using FindExpert.UI.Api.Models.Advertisement.Update;
using FindExpert.UI.Api.Models.Authorization.ConfirmEmail;
using FindExpert.UI.Api.Models.Authorization.Login;
using FindExpert.UI.Api.Models.Authorization.Register;
using FindExpert.UI.Api.Models.Base;
using FindExpert.UI.Api.Models.Categories.GetList;
using FindExpert.UI.Api.Models.Responses.Create;
using FindExpert.UI.Api.Models.Responses.GetMy;
using FindExpert.UI.Api.Models.Responses.Update;

namespace FindExpert.UI.Api.Services;

internal sealed class MasterOkApiHttpClient(HttpClient httpClient):IMasterOkApi
{
    private readonly JsonSerializerOptions _jsonOptions = new()
                                                          {
                                                              PropertyNameCaseInsensitive = true,
                                                              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                                                          };

    public Task<ApiResponse<LoginResponse>> Login(LoginRequest request) => SendRequest<LoginRequest, LoginResponse>(
                                                                                                                    "/authorization/login",
                                                                                                                    HttpMethod.Post,
                                                                                                                    request);

    public Task<ApiResponse> Register(RegisterRequest request) => SendRequest(
                                                                              "/user/register",
                                                                              HttpMethod.Post,
                                                                              request);

    public Task<ApiResponse> ConfirmEmail(ConfirmEmailRequest request) => SendRequest(
                                                                                      "/user/confirm-email",
                                                                                      HttpMethod.Post,
                                                                                      request);

    public Task<ApiResponse> CreateAdvertisement(CreateAdvertisementRequest request, string accessToken) => SendRequest(
                                                                                                                        "/advertisement",
                                                                                                                        HttpMethod.Post,
                                                                                                                        request,
                                                                                                                        accessToken);

    public Task<ApiResponse> UpdateAdvertisement(int id, UpdateAdvertisementRequest request, string accessToken) => SendRequest(
                                                                                                                                $"/advertisement/{id}",
                                                                                                                                HttpMethod.Put,
                                                                                                                                request,
                                                                                                                                accessToken);

    public Task<ApiResponse> DeleteAdvertisement(int id, string accessToken) => SendRequest(
                                                                                            $"/advertisement/{id}",
                                                                                            HttpMethod.Delete,
                                                                                            accessToken);

    public Task<ApiResponse<GetCategoriesResponse>> GetAdvertisementCategories(string accessToken) => SendRequest<GetCategoriesResponse>(
                                                                                                                                         "/categories",
                                                                                                                                         HttpMethod.Get,
                                                                                                                                         accessToken);

    public Task<ApiResponse<GetMyAdvertisementListResponse>> GetMyAdvertisementList(string accessToken) => SendRequest<GetMyAdvertisementListResponse>("/advertisement/my", HttpMethod.Get, accessToken);

    public Task<ApiResponse<GetAdvertisementListResponse>> SearchAdvertisement(string searchText, int categoryId, string accessToken) =>
        SendRequest<GetAdvertisementListResponse>($"/advertisement/search?CategoryId={categoryId}&SearchText={searchText}", HttpMethod.Get, accessToken);

    public Task<ApiResponse<AdvertisementInfo>> GetAdvertisementInfo(int id, string accessToken) => SendRequest<AdvertisementInfo>($"/advertisement/{id}", HttpMethod.Get, accessToken);

    public Task<ApiResponse<AdvertisementInfoWithResponsesWithResponseFlag>> GetAdvertisementWithResponsesAndResponseFlagInfo(int id, string accessToken) =>
        SendRequest<AdvertisementInfoWithResponsesWithResponseFlag>($"/advertisement/{id}?IncludeResponses=true", HttpMethod.Get, accessToken);

    public Task<ApiResponse<AdvertisementWithResponses>> GetAdvertisementWithResponses(int id, string accessToken) => SendRequest<AdvertisementWithResponses>($"/advertisement/{id}?IncludeResponses=true", HttpMethod.Get, accessToken);
    public Task<ApiResponse> CreateResponse(CreateResponseRequest request, string accessToken) => SendRequest("/response", HttpMethod.Post, request, accessToken);
    public Task<ApiResponse<GetMyResponseListResponse>> GetMyResponseList(string accessToken) => SendRequest<GetMyResponseListResponse>("/response/my", HttpMethod.Get, accessToken);
    public Task<ApiResponse> DeleteResponse(int id, string accessToken) => SendRequest($"/response/{id}", HttpMethod.Delete, accessToken);
    public Task<ApiResponse> UpdateResponse(int id, UpdateResponseRequest request, string accessToken) => SendRequest($"/response/{id}", HttpMethod.Put, request, accessToken);

    private async Task<ApiResponse<TResponse>> SendRequest<TRequest, TResponse>(
        string endpoint,
        HttpMethod method,
        TRequest request,
        string? token = null)
    {
        try
        {
            var httpRequest = new HttpRequestMessage(method, endpoint)
                              {
                                  Content = JsonContent.Create(request, options: _jsonOptions)
                              };

            if (!string.IsNullOrEmpty(token))
                httpRequest.Headers.Authorization = new("Bearer", token);

            var response = await httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponse<TResponse>>(content, _jsonOptions);

            if (apiResponse is null)
                throw new("Could not deserialize response");

            return response.IsSuccessStatusCode switch
            {
                false when apiResponse.Error is null => throw new("Response was not successful but error is null"),
                true when apiResponse.Data is null => throw new("Response was successful but data is null"),
                _ => apiResponse
            };

        }
        catch (Exception)
        {
            return new()
                   {
                       Error = new() { ErrorCode = "NETWORK_ERROR" }
                   };
        }
    }

    private async Task<ApiResponse> SendRequest<TRequest>(
        string endpoint,
        HttpMethod method,
        TRequest request,
        string? token = null)
    {
        try
        {
            var httpRequest = new HttpRequestMessage(method, endpoint)
                              {
                                  Content = JsonContent.Create(request, options: _jsonOptions)
                              };

            if (!string.IsNullOrEmpty(token))
                httpRequest.Headers.Authorization = new("Bearer", token);

            var response = await httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(content, _jsonOptions);

            if (apiResponse is null)
                throw new("Could not deserialize response");

            if (!response.IsSuccessStatusCode && apiResponse.Error is null)
                throw new("Response wan not successful but error is null");

            return apiResponse;
        }
        catch (Exception)
        {
            return new()
                   {
                       Error = new() { ErrorCode = "NETWORK_ERROR" }
                   };
        }
    }

    private async Task<ApiResponse> SendRequest(
        string endpoint,
        HttpMethod method,
        string? token = null)
    {
        try
        {
            var httpRequest = new HttpRequestMessage(method, endpoint);

            if (!string.IsNullOrEmpty(token))
                httpRequest.Headers.Authorization = new("Bearer", token);

            var response = await httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(content, _jsonOptions);

            if (apiResponse is null)
                throw new("Could not deserialize response");

            if (!response.IsSuccessStatusCode && apiResponse.Error is null)
                throw new("Response wan not successful but error is null");

            return apiResponse;
        }
        catch (Exception)
        {
            return new()
                   {
                       Error = new() { ErrorCode = "NETWORK_ERROR" }
                   };
        }
    }

    private async Task<ApiResponse<TResponse>> SendRequest<TResponse>(
        string endpoint,
        HttpMethod method,
        string? token = null)
    {
        try
        {
            var httpRequest = new HttpRequestMessage(method, endpoint);

            if (!string.IsNullOrEmpty(token))
                httpRequest.Headers.Authorization = new("Bearer", token);

            var response = await httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponse<TResponse>>(content, _jsonOptions);

            if (apiResponse is null)
                throw new("Could not deserialize response");

            if (!response.IsSuccessStatusCode && apiResponse.Error is null)
                throw new("Response wan not successful but error is null");

            return apiResponse;
        }
        catch (Exception)
        {
            return new()
                   {
                       Error = new() { ErrorCode = "NETWORK_ERROR" }
                   };
        }
    }
}