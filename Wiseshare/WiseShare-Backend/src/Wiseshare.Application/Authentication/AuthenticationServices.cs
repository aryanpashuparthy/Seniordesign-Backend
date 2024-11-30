
using FluentResults;
using Wiseshare.Application.Common.Interfaces.Authentication;
using Wiseshare.Application.Repository;
using Wiseshare.Domain.UserAggregate;
using Wiseshare.Domain.UserAggregate.ValueObjects;
using WiseShare.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public Result Register(string firstName, string lastName, string email, string phone, string password)
    {
        //check if user already exists

        //create user(generate uniqie id)

        //create jwtToke
        var userId = UserId.CreateUnique();



        var user = User.Create(firstName, lastName, email, phone, password); //create the user from passed in values
        var token = _jwtTokenGenerator.GenerateToken(user);

        _userRepository.Insert(user); //save the user to the repo

        return Result.Ok();
    }

    public Result<(string Token, string FirstName, string LastName)> Login(string email, string password)
    {
        var userResult = _userRepository.GetUserByEmail(email);

        // Check if the user exists
        if (userResult.IsFailed)
        {
            return Result.Fail("Invalid email or password.");
        }

        // check the password
        var user = userResult.Value;
        if (user.Password != password)
        {
            return Result.Fail("Invalid email or password.");
        }

        //return Result.Ok();

        //test
        var token1 = _jwtTokenGenerator.GenerateToken(user);

        return Result.Ok((token1, user.FirstName, user.LastName));

    }

}
