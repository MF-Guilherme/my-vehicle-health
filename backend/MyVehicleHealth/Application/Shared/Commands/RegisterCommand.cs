using MediatR;
        using Microsoft.AspNetCore.Identity;
        using MyVehicleHealth.Application.Shared.Dtos;
        using MyVehicleHealth.Domain.Entities;
        
        namespace MyVehicleHealth.Application.Shared.Commands;
        
        public record RegisterCommand(RegisterDto Dto) : IRequest<string>;
        
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
        {
            private readonly UserManager<User> _userManager;
        
            public RegisterCommandHandler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }
        
            public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var user = new User { Name = request.Dto.Name, Email = request.Dto.Email };
                var result = await _userManager.CreateAsync(user, request.Dto.Password);
        
                if (result.Succeeded)
                {
                    return "User registered successfully";
                }
        
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }