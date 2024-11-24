using Wiseshare.Application.Repository;
using Wiseshare.Domain.UserAggregate;
using Wiseshare.Domain.UserAggregate.ValueObjects;

namespace Wiseshare.Application.services.UserServices;


public class UserService : IUserService
{

    //private readonly List<User> _users;

    /*
        public UserService()
        {
            _users = new List<User>{
                User.Create("Ali", "Arthur", "ali@gmail.com", "814-218-1111", "password123"),
                User.Create("John", "Smith", "ali2@gmail.com", "814-218-2222", "password456"),
                User.Create("Jane", "Doe", "ali3@gmail.com", "814-218-3333", "password678")
            };
        }
        */

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? GetUserById(Guid userId)
    {
        return _userRepository.Read(userId);

        //return _users.FirstOrDefault(user => user.Id.Value == userId);//user represent each individual item in the _list => means do this which is comparing
        //how the function would look like without using lambda function
        /*foreach (var user in _users)
    {
        if (user.Id.Value == userId)
        {
            return user;
        }
    }
    return null; */
    }
    public User? GetUserByEmail(string email)
    {
        //find the first user with a matching email then stop searching
        //return _users.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        return _userRepository.GetUserByEmail(email);
    }

    public User? GetUserByPhone(string phone)
    {
        //find the user with the first matching phone number
        //return _users.FirstOrDefault(user => user.Phone == phone);
        return _userRepository.GetUserByPhone(phone);
    }

    public User? CreateUser(User user)
    {
        var newUser = User.Create(
            user.FirstName,
            user.LastName,
            user.Email,
            user.Phone,
            user.Password
        );
        return _userRepository.Create(newUser);
    }
    public User? UpdateUser(UserId userId, string? email = null, string? phone = null, string? password = null)
    {
        if (userId is null) return null; // Check if userId exist if not return null 

        // Retrieve the existing user object from the repository
        var user = _userRepository.Read(userId.Value);
        if (user is null) return null;

        //checks if the user Provide information to update if not return the current value by using the getter
        var updatedEmail = !string.IsNullOrWhiteSpace(email) ? email : user.Email;
        var updatedPhone = !string.IsNullOrWhiteSpace(phone) ? phone : user.Phone;
        var updatedPassword = !string.IsNullOrWhiteSpace(password) ? password : user.Password;

        // Call the repository to update the user with the updated values
        return _userRepository.Update(userId, updatedEmail, updatedPhone, updatedPassword);
    }
    public User? DeleteUser(string userId)
{
    //delete a user by passed in ID
    return _userRepository.Delete(userId);
}



}
