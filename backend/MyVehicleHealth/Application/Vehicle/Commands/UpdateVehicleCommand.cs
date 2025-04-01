using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Vehicle.Commands;

public record UpdateVehicleCommand(int Id, VehicleUpdateDto Dto) : IRequest<int>;

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand,int>
{
    private readonly AppDbContext _context;

    public UpdateVehicleCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (vehicle is null)
        {
            throw new Exception("Veículo não encontrado");
        }

        vehicle.Name = request.Dto.Name;
        await _context.SaveChangesAsync(cancellationToken);

        return vehicle.Id; // Retorna o Id do veículo
    }
}