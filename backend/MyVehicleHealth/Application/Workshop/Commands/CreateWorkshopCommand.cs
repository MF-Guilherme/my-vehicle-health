using MediatR;
using MyVehicleHealth.Application.Workshop.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Workshop.Commands;

public record CreateWorkshopCommand(WorkshopCreateDto Dto) : IRequest<int>;

public class CreateWorkshopCommandHandler : IRequestHandler<CreateWorkshopCommand, int>
{
    private readonly AppDbContext _context;

    public CreateWorkshopCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateWorkshopCommand request,
        CancellationToken cancellationToken)
    {
        var workshop = new Domain.Entities.Workshop
        {
            CompanyName = request.Dto.CompanyName,
            MechanicName = request.Dto.MechanicName,
            Phone = request.Dto.Phone
        };
        
        _context.Workshops.Add(workshop);
        await _context.SaveChangesAsync(cancellationToken);
        return workshop.Id;
    }
}