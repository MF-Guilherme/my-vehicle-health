using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Maintenance.Queries;

public record GetMaintenanceByIdQuery(int Id) : IRequest<MaintenanceReadDto>;

public class GetMaintenanceByIdQueryHandler : IRequestHandler<GetMaintenanceByIdQuery, MaintenanceReadDto>
{
    private readonly AppDbContext _context;

    public GetMaintenanceByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<MaintenanceReadDto> Handle(GetMaintenanceByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Maintenances
            .Include(m => m.Vehicle)
            .Include(m => m.Workshop)
            .Include(m => m.Services)
            .Where(m => m.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
    
}