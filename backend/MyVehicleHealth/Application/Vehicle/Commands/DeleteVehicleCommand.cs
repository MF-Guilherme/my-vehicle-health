using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Vehicle.Commands;

public record DeleteVehicleCommand(int Id) : IRequest<Unit>; // Unit = void do MediatR

public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Unit>
{
    private readonly AppDbContext _context;

    public DeleteVehicleCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (vehicle is not null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}