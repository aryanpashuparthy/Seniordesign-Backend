using Wiseshare.Domain.UserAggregate;
using Wiseshare.Domain.UserAggregate.ValueObjects;
namespace Wiseshare.Application.Repository;
public interface IUserRepository{


public User? GetUserByEmail(string UserEmail);
public User? GetUserByPhone(string UserPhone);
public User? Create(User User);
public User? Read(Guid UserId);
public User? Delete(string User);
public User? Update(UserId UserID, string email, string phone, string Password);
}