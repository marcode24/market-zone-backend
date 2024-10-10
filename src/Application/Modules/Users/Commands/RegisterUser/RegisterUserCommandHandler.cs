namespace Application.Modules.Users.Commands.RegisterUser;

using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Domain.Repositories.Roles;
using Domain.Repositories.Users;
using Domain.Shared.ValueObjects;

internal class RegisterUserCommandHandler
  : ICommandHandler<RegisterUserCommand, int>
{
  private readonly IUserRepository _userRepository;
  private readonly IRoleRepository _roleRepository;
  private readonly IUnitOfWork _unitOfWork;

  public RegisterUserCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _roleRepository = roleRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
  {
    var roleExists = await _roleRepository.ExistsByIdAsync(new RoleId(request.RoleId), cancellationToken);

    if (!roleExists)
      return Result.Failure<int>(RoleErrors.NotFound);

    var email = new Email(request.Email);
    var userExists = await _userRepository.ExistsByEmailAsync(email, cancellationToken);

    if (userExists)
      return Result.Failure<int>(UserErrors.AlreadyExists);

    var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

    var user = User.Create(
      new Name(request.Name),
      new LastName(request.LastName),
      new Email(request.Email),
      new Password(passwordHash),
      new Photo(request.Photo ?? string.Empty)
    );

    _userRepository.CreateAsync(user, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Result.Success(user.Id!.Value);
  }
}
