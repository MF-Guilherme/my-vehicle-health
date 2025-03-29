using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Application.Vehicle.Queries;
using MyVehicleHealth.Application.Workshop.Dtos;
using MyVehicleHealth.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MyVehicleHealth.Application.Workshop.Queries;

public record GetWorkshopByIdQuery(int Id) : IRequest<WorkshopReadDto>;

public class GetWorkshopByIdQueryHandler : IRequestHandler<GetWorkshopByIdQuery, WorkshopReadDto>
{
    private readonly AppDbContext _context;

    public GetWorkshopByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WorkshopReadDto> Handle(GetWorkshopByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Workshops
            .Where(w => w.Id == request.Id)
            .Include(w => w.Maintenances)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    
}

