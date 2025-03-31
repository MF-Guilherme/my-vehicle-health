using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Service.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Service.Queries;

public record GetAllServicesForMaintenanceQuery(int MaintenanceId) : IRequest<List<ServiceReadDto>>;

public class GetAllServicesForMaintenanceQueryHandler : IRequestHandler<GetAllServicesForMaintenanceQuery, List<ServiceReadDto>>
{
    private readonly AppDbContext _context;

    public GetAllServicesForMaintenanceQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ServiceReadDto>> Handle(GetAllServicesForMaintenanceQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Services
            .Where(s => s.Maintenance.Id == request.MaintenanceId)
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
            .ToListAsync(cancellationToken); 
    }
    
}