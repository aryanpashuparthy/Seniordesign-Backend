using FluentResults;
using Moq;
using Wiseshare.Application.Repository;
using Wiseshare.Application.services;
using Wiseshare.Application.services.UserServices;
using Wiseshare.Domain.UserAggregate;
using Wiseshare.Domain.UserAggregate.ValueObjects;

public class UserServiceTests
{
    //Mock<IUserRepository> is a fake version of IUserRepository. it mimics the behavior of a
    //real repository without connecting to a actual database
    private readonly Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
    private IUserService _userServicee; //hold an instance of the UserService being tested
    public UserServiceTests()//passes the mock object of IUserRepo into user service so that the service interacts with the fake repo
    {
        _userServicee = new UserService(_userRepoMock.Object);
    }
    [Fact]//marks the following method as a test method
    public void Test_GetUserById_WhenUserIdIsValid()
    {
        //Arrange
        var userId = UserId.CreateUnique();
        var user1 = User.Create("ali", "arthur", "user1@gmail.com", "89324823479", "password");
        _userRepoMock.Setup(x => x.GetUserById(userId))
        .Returns(user1);

        //Act
        var result = _userServicee.GetUserById(userId);
        //assert
        Assert.True(result.IsSuccess);
        Assert.Equal("ali", result.Value.FirstName); //(expected , then type)
    }

    [Fact]
    public void Test_GetUserByEmail_WhenUserEmailIsValid()
    {
        var userId = UserId.CreateUnique();
        var useremail = "ali@gmail.com";
        var user2 = User.Create("ali", "arthur", useremail, "8122987984", "password");
        _userRepoMock.Setup(x => x.GetUserByEmail(useremail))
        .Returns(user2);

        var result = _userServicee.GetUserByEmail(useremail);

        Assert.True(result.IsSuccess);
        Assert.Equal("ali@gmail.com", result.Value.Email);

    }

    [Fact]
    public void Test_GetUserById_WhenUserIdIsInvalid()
    {
        // Arrange
        var userId = UserId.CreateUnique();
        _userRepoMock.Setup(x => x.GetUserById(userId))
            .Returns(Result.Fail<User>("User not found"));//tells mock to return a failed Result<user>

        // Act
        var result = _userServicee.GetUserById(userId);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("User not found", result.Errors.First().Message);
    }

    [Fact]
    public void Test_GetUserByPhone_WhenUserPhoneIsValid()
    {
        //Arrange
        var userId = UserId.CreateUnique();
        var userPhone = "123456789";
        var user3 = User.Create("ali3", "Arthur4", "ali3@gmail.com", userPhone, "password3");
        _userRepoMock.Setup(x => x.GetUserByPhone(userPhone))
        .Returns(user3);

        //Act
        var result = _userServicee.GetUserByPhone(userPhone);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("123456789", result.Value.Phone);

    }

    [Fact]
    public void Test_GetUsers_WhenUserIsValid()
    {
        var users = new List<User>{
            User.Create("ali","arthur","ali@gmail.com","123456789","password4"),
            User.Create("ali2","arthur2","ali2@gmail.com","123456789","password4"),
            User.Create("ali","arthur","ali@gmail.com","123456789","password4")
        };
        _userRepoMock.Setup(x => x.GetUsers())
        .Returns(users);

        //act
        var result = _userServicee.GetUsers();

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value.Count());//checks the amount of users mathc those in the list

        // Check the first user in the list
        var firstUser = result.Value.ElementAt(0); // or result.Value.First()
        Assert.Equal("ali", firstUser.FirstName);
        Assert.Equal("arthur", firstUser.LastName);
        Assert.Equal("ali@gmail.com", firstUser.Email);

        // Check the second user in the list
        var secondUser = result.Value.ElementAt(1);
        Assert.Equal("ali2", secondUser.FirstName);
        Assert.Equal("arthur2", secondUser.LastName);
        Assert.Equal("ali2@gmail.com", secondUser.Email);
    }

}
