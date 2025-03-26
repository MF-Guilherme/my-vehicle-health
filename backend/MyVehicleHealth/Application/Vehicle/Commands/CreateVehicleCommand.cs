using FluentValidation;
using MediatR;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Domain.Entities;
using MyVehicleHealth.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MyVehicleHealth.Application.Vehicle.Commands;

public record CreateVehicleCommand(VehicleCreateDto Dto) : IRequest<Domain.Entities.Vehicle>;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Domain.Entities.Vehicle>
{
    private readonly AppDbContext _context;

    public CreateVehicleCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = new Domain.Entities.Vehicle
        {
            Name = request.Dto.Name
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync(cancellationToken);

        return vehicle;
    }
}

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("O nome do veículo é obrigatório")
            .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres");
    }
}