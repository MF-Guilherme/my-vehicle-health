using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Application.Workshop.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Workshop.Queries;

public record GetAllWorkshopsQuery : IRequest<List<WorkshopReadDto>>;

public class GetAllWorkshopsQueryHandler : IRequestHandler<GetAllWorkshopsQuery, List<WorkshopReadDto>>
{
    private readonly AppDbContext _context;

    public GetAllWorkshopsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkshopReadDto>> Handle(GetAllWorkshopsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Workshops
            .Select(w => new WorkshopReadDto
            {
                Id = w.Id,
                CompanyName = w.CompanyName,
                MechanicName = w.MechanicName,
                Phone = w.Phone,
                Maintenances = w.Maintenances.Select(m => new WorkshopMaintenanceSummaryDto
                {
                    Id = m.Id,
                    VehicleName = m.Vehicle.Name,
                    MaintenanceDate = m.MaintenanceDate,
                    Services = m.Services.Select(s => new ServiceSummaryReadDto
                    {
                        Id = s.Id,
                        Description = s.Description,
                    }).ToList(),
                    TotalCost = m.Services.Sum(s => s.PartCost + s.LaborCost),
                }).ToList(),
            })
            .ToListAsync(cancellationToken);
    }
    
}