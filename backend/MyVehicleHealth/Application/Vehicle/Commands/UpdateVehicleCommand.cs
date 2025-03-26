using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Domain.Entities;
using MyVehicleHealth.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MyVehicleHealth.Application.Vehicle.Commands;

public record UpdateVehicleCommand(int Id, VehicleUpdateDto Dto) : IRequest<Domain.Entities.Vehicle>;

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Domain.Entities.Vehicle>
{
    private readonly AppDbContext _context;

    public UpdateVehicleCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Vehicle> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (vehicle is null)
        {
            throw new Exception("Veículo não encontrado"); // Ou crie uma exceção customizada
        }

        vehicle.Name = request.Dto.Name;
        await _context.SaveChangesAsync(cancellationToken);

        return vehicle; // Retorna o veículo atualizado
    }
}