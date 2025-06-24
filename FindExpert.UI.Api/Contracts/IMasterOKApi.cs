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

namespace FindExpert.UI.Api.Contracts;

public interface IMasterOkApi
{
    Task<ApiResponse<LoginResponse>> Login(LoginRequest request);

    Task<ApiResponse> Register(RegisterRequest request);

    Task<ApiResponse> ConfirmEmail(ConfirmEmailRequest request);
    Task<ApiResponse> CreateAdvertisement(CreateAdvertisementRequest request, string accessToken);
    Task<ApiResponse> UpdateAdvertisement(int id, UpdateAdvertisementRequest request, string accessToken);
    Task<ApiResponse> DeleteAdvertisement(int id, string accessToken);
    Task<ApiResponse<GetCategoriesResponse>> GetAdvertisementCategories(string accessToken);
    Task<ApiResponse<GetMyAdvertisementListResponse>> GetMyAdvertisementList(string accessToken);
    Task<ApiResponse<GetAdvertisementListResponse>> SearchAdvertisement(string searchText, int categoryId, string accessToken);
    Task<ApiResponse<AdvertisementInfo>> GetAdvertisementInfo(int id, string accessToken);
    Task<ApiResponse<AdvertisementInfoWithResponsesWithResponseFlag>> GetAdvertisementWithResponsesAndResponseFlagInfo(int id, string accessToken);
    Task<ApiResponse<AdvertisementWithResponses>> GetAdvertisementWithResponses(int id, string accessToken);
    Task<ApiResponse> CreateResponse(CreateResponseRequest request, string accessToken);
    Task<ApiResponse<GetMyResponseListResponse>> GetMyResponseList(string accessToken);
    Task<ApiResponse> DeleteResponse(int id, string accessToken);
    Task<ApiResponse> UpdateResponse(int id, UpdateResponseRequest request, string accessToken);
}