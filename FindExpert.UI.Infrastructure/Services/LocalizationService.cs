using FindExpert.UI.Api.Models.Base;
using FindExpert.UI.Infrastructure.Resources.Localization;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.Infrastructure.Services;

internal sealed class LocalizationService:ILocalizationService
{
    public string GetLocalizedString(BaseProblemDetails problemDetails)
    {
        if (problemDetails.ErrorCode is null)
            return !string.IsNullOrEmpty(problemDetails.Detail)? problemDetails.Detail : "При запросе произошла непредвиденная ошибочка";

        var localizedString = Errors.ResourceManager.GetString(problemDetails.ErrorCode);

        if (localizedString is not null)
            return localizedString;

        return !string.IsNullOrEmpty(problemDetails.Detail)? problemDetails.Detail : "При запросе произошла непредвиденная ошибочка";
    }
}