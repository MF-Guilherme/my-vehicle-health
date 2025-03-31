using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Service.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Service.Commands;

public record UpdateServiceCommand(int Id, ServiceUpdateDto Dto) : IRequest<int>;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, int>
{
    private readonly AppDbContext _context;

    public UpdateServiceCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        
        if (service is null)
        {
            throw new Exception("Service not found");
        }

        service.Description = request.Dto.Description;
        service.MaintenanceDate = request.Dto.MaintenanceDate;
        service.CurrentMileage = request.Dto.CurrentMileage;
        service.NextMaintenanceDate = request.Dto.NextMaintenanceDate;
        service.NextMaintenanceMileage = request.Dto.NextMaintenanceMileage;
        service.PartBrand = request.Dto.PartBrand;
        service.PartCost = request.Dto.PartCost;
        service.LaborCost = request.Dto.LaborCost;
        await _context.SaveChangesAsync(cancellationToken);
        return service.Id;
    }
    
}