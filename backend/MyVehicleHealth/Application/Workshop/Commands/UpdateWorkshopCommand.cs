using MediatR;
using MyVehicleHealth.Application.Workshop.Dtos;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Workshop.Commands;

public record UpdateWorkshopCommand(int Id, WorkshopUpdateDto Dto) : IRequest<Domain.Entities.Workshop>;

public class UpdateWorkshopCommandHandler : IRequestHandler<UpdateWorkshopCommand, Domain.Entities.Workshop>
{
    private readonly AppDbContext _context;

    public UpdateWorkshopCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Workshop> Handle(UpdateWorkshopCommand request,
        CancellationToken cancellationToken)
    {
        var workshop = await _context.Workshops
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
        if (workshop is null)
        {
            throw new Exception("Oficina não encontrada");
        }
        
        workshop.CompanyName = request.Dto.CompanyName;
        workshop.MechanicName = request.Dto.MechanicName;
        workshop.Phone = request.Dto.Phone;
        
        await _context.SaveChangesAsync(cancellationToken);
        return workshop;
    }
}