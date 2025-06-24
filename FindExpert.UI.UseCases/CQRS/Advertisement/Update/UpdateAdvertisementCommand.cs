using Ardalis.SharedKernel;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Update;

public readonly record struct UpdateAdvertisementCommand(int Id, string Name, string Description, int AdvertisementCategoryId):ICommand<OperationResult>;