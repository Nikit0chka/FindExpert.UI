using FindExpert.UI.Api.Models.Base;

namespace FindExpert.UI.UseCases.Contracts;

public interface ILocalizationService
{
    string GetLocalizedString(BaseProblemDetails problemDetails);
}