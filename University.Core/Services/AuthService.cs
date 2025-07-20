using Microsoft.AspNetCore.Identity;
using University.Core.DTOs;
using University.Core.Entities;
using University.Core.Entities.Identity;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Interfaces;
using University.Core.Validations;



namespace University.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStudentRepository _studentRepository;

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager, IStudentRepository studentRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _studentRepository = studentRepository;
        }
        public async Task<UserDTO> Register(RegisterForm request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var validation = FormValidator.Validate(request);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);


            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                throw new BusinessException("User already exists with this email.");

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new BusinessException(result.Errors
                    .GroupBy(x => x.Code)
                    .ToDictionary(x => x.Key, y => y.Select(a => a.Description).ToList()));

            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(request.Role))
                await _roleManager.CreateAsync(new Role { Name = request.Role });

            await _userManager.AddToRoleAsync(user, request.Role);

            if (request.Role == "Student")
            {
                var student = new Student
                {
                    Name = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    UserId = user.Id
                    
                };

                 _studentRepository.Add(student);
            }

            return new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Phone = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                Role = request.Role
            };
        }


        
        public async Task<UserDTO> Login(LoginForm form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);

            var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(form.Email);
                if (user == null)
                    throw new NotFoundException($"Unable to find account with email {form.Email}.");

                var roles = await _userManager.GetRolesAsync(user);

                var dto = new UserDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Phone = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    Role= roles.FirstOrDefault() ?? "Student"
                };

                return dto;
            }
            else if (result.IsLockedOut)
            {
                throw new BusinessException("Account is locked out.");
            }
            else if (result.IsNotAllowed)
            {
                throw new BusinessException("Account is not allowed to login.");
            }

            throw new BusinessException("Invalid login attempt.");
        }

        
    }

    public interface IAuthService
    {
        Task<UserDTO> Login(LoginForm request);
        Task<UserDTO> Register(RegisterForm request);
    }
}