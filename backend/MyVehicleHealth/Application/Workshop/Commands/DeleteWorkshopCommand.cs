using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MyVehicleHealth.Application.Workshop.Commands;

public record DeleteWorkshopCommand(int Id) : IRequest<Unit>;

public class DeleteWorkshopCommandHandler : IRequestHandler<DeleteWorkshopCommand, Unit>
{
    private readonly AppDbContext _context;

    public DeleteWorkshopCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteWorkshopCommand request, CancellationToken cancellationToken)
    {
        var workshop = await _context.Workshops
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

        if (workshop is not null)
        {
            _context.Workshops.Remove(workshop);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        return Unit.Value;
    }
    
}