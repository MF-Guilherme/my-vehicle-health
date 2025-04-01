using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Queries;
using MyVehicleHealth.Application.Service.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Service.Queries;

public record GetServiceByIdQuery(int Id) : IRequest<ServiceReadDto>;

public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceReadDto>
{
    private readonly AppDbContext _context;

    public GetServiceByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceReadDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Services
            .Where(s => s.Id == request.Id)
            .Include(s => s.Maintenance)
            .Select(s => new ServiceReadDto
            {
                Id = s.Id,
                MaintenanceId = s.MaintenanceId,
                Description = s.Description,
                PartCost = s.PartCost,
                LaborCost = s.LaborCost,
                MaintenanceDate = s.MaintenanceDate,
                NextMaintenanceDate = s.NextMaintenanceDate,
                CurrentMileage = s.CurrentMileage,
                NextMaintenanceMileage = s.NextMaintenanceMileage,
                PartBrand = s.PartBrand
            })
            .FirstOrDefaultAsync(cancellationToken);
        
    }
    
}