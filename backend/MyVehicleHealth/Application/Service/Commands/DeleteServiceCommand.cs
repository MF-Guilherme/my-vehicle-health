using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Service.Commands;

public record DeleteServiceCommand(int Id) : IRequest<Unit>;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, Unit>
{
    private readonly AppDbContext _context;

    public DeleteServiceCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        
        if (service is null) return Unit.Value;
        
        _context.Services.Remove(service);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
    
}