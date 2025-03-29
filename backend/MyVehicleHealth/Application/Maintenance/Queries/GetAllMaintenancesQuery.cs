using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Application.Service.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Maintenance.Queries;

public record GetAllMaintenancesQuery : IRequest<List<MaintenanceReadDto>>;

public class GetAllMaintenancesQueryHandler : IRequestHandler<GetAllMaintenancesQuery, List<MaintenanceReadDto>>
{
    private readonly AppDbContext _context;

    public GetAllMaintenancesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MaintenanceReadDto>> Handle(GetAllMaintenancesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Maintenances
            .Include(m => m.Vehicle)
            .Include(m => m.Workshop)
            .Include(m => m.Services)
            .Select(m => new MaintenanceReadDto
            {
                Id = m.Id,
                VehicleName = m.Vehicle.Name,
                WorkshopName = m.Workshop.CompanyName,
                MaintenanceDate = m.MaintenanceDate,
                TotalCost = m.Services.Sum(s => s.PartCost + s.LaborCost),
                Services = m.Services.Select(s => new ServiceSummaryReadDto
                {
                    Id = s.Id,
                    Description = s.Description,
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}