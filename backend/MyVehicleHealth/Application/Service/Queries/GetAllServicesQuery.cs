using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Service.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Service.Queries;

// Query (recebe os filtros via DTO)
public record GetAllServicesQuery(ServiceFilterDto Filter) : IRequest<List<ServiceReadDto>>;

// Handler
public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, List<ServiceReadDto>>
{
    private readonly AppDbContext _context;

    public GetAllServicesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ServiceReadDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Services
            .Include(s => s.Maintenance)
            .AsQueryable(); // Permite adicionar filtros dinamicamente
        
        // Filtro por descrição (LIKE)
        if (!string.IsNullOrEmpty(request.Filter.Description))
        {
            query = query.Where(s => s.Description.Contains(request.Filter.Description));
        }
        
        // Filtro por data de manutenção
        if (request.Filter.MaintenanceDate.HasValue)
        {
            query = query.Where(s => s.MaintenanceDate.Date == request.Filter.MaintenanceDate.Value.Date);
        }
        
        // Filtro por data da próxima manutenção
        if (request.Filter.NextMaintenanceDate.HasValue)
        {
            query = query.Where(s => s.NextMaintenanceDate.Date == request.Filter.NextMaintenanceDate.Value.Date);
        }
        
        return await query
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