using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;
using MyVehicleHealth.Application.Maintenance.Dtos;

namespace MyVehicleHealth.Application.Vehicle.Queries;

// Record (definição da query)
public record GetVehicleByIdQuery(int Id) : IRequest<VehicleReadDto>;

// Handler (implementação)
public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleReadDto>
{
    private readonly AppDbContext _context;
    
    public GetVehicleByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<VehicleReadDto> Handle(
        GetVehicleByIdQuery request, 
        CancellationToken cancellationToken)
    {
        return await _context.Vehicles
            .Include(v => v.Maintenances)
            .ThenInclude(m => m.Services)
            .Where(v => v.Id == request.Id)
            .Select(v => new VehicleReadDto
            {
                Id = v.Id,
                Name = v.Name,
                Maintenances = v.Maintenances.Select(m => new MaintenanceSummaryDto
                {
                    Id = m.Id,
                    MaintenanceDate = m.MaintenanceDate,
                    WorkshopName = m.Workshop.CompanyName,
                    Services = m.Services.Select(s => new ServiceSummaryReadDto
                    {
                        Id = s.Id,
                        Description = s.Description,
                    }).ToList(),
                    TotalCost = m.Services.Sum(s => s.PartCost + s.LaborCost)
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}