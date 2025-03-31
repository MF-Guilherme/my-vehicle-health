using MediatR;
using MyVehicleHealth.Application.Service.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Service.Commands;

public record CreateServiceCommand(int MaintenanceId, ServiceCreateDto Dto) : IRequest<int>;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
{
    private readonly AppDbContext _context;

    public CreateServiceCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Domain.Entities.Service
        {
            MaintenanceId = request.MaintenanceId,
            Description = request.Dto.Description,
            MaintenanceDate = request.Dto.MaintenanceDate,
            CurrentMileage = request.Dto.CurrentMileage,
            NextMaintenanceDate = request.Dto.NextMaintenanceDate,
            NextMaintenanceMileage = request.Dto.NextMaintenanceMileage,
            PartBrand = request.Dto.PartBrand,
            PartCost = request.Dto.PartCost,
            LaborCost = request.Dto.LaborCost,
        };
        _context.Services.Add(service);
        await _context.SaveChangesAsync(cancellationToken);
        return service.Id;
    }
}