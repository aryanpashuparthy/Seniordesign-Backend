
using Wiseshare.Domain.UserAggregate;
using Wiseshare.Domain.UserAggregate.ValueObjects;

namespace Wiseshare.Application.services;

public interface IUserService {

public User? GetUserById(Guid userId);

public User? GetUserByEmail(string UserEmail);

public User? GetUserByPhone(string UserPhone);
}