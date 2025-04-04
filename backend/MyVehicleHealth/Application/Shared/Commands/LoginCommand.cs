using MediatR;
    using Microsoft.AspNetCore.Identity;
    using MyVehicleHealth.Application.Shared.Dtos;
    using MyVehicleHealth.Domain.Entities;
    
    namespace MyVehicleHealth.Application.Shared.Commands;
    
    public record LoginCommand(LoginDto Dto) : IRequest<string>;
    
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
    
        public LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
    
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Dto.Email);
            if (user is null)
            {
                throw new Exception("Email not registered.");
            }
    
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Dto.Password, false);
            if (result.Succeeded)
            {
                return "User logged in successfully";
            }
    
            throw new Exception("Invalid login attempt.");
        }
    }