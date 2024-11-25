
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
        var user1 = User.Create("ali","arthur", "aldk.gmail.com", "89324823479", "ladkfjllkdf");
        _userRepoMock.Setup(x => x.GetUserById(userId))
        .Returns(user1);

        //Act
        var result = _userServicee.GetUserById(userId);
        Assert.True(result.IsSuccess);
        Assert.Equal("ali", result.Value.FirstName); //(expected , then type)

    }
}
