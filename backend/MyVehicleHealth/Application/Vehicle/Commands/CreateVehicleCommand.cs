using FluentValidation;
using MediatR;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Vehicle.Commands;

public record CreateVehicleCommand(VehicleCreateDto Dto) : IRequest<int>;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, int>
{
    private readonly AppDbContext _context;

    public CreateVehicleCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = new Domain.Entities.Vehicle
        {
            Name = request.Dto.Name
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync(cancellationToken);

        return vehicle.Id;
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