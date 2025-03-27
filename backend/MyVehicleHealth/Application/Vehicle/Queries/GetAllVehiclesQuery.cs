using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyVehicleHealth.Application.Maintenance.Dtos;

namespace MyVehicleHealth.Application.Vehicle.Queries;

public record GetAllVehiclesQuery : IRequest<List<VehicleReadDto>>;

public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, List<VehicleReadDto>>
{
    private readonly AppDbContext _context;

    public GetAllVehiclesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleReadDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Vehicles
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
            .ToListAsync(cancellationToken);
    }
}