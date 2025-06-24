using Ardalis.SharedKernel;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Create;

public readonly record struct CreateAdvertisementCommand(string Name, string Description, int AdvertisementCategoryId):ICommand<OperationResult>;