using LoginTestsApp.Console;
using Moq;
using Shouldly;

namespace LoginTestsApp.Tests
{
    public class SignInViewModelTests
    {
        private Mock<IUserService> _userService;
        private Mock<IPageService> _pageService;
        public SignInViewModelTests()
        {
            _userService = new Mock<IUserService>();
            _pageService = new Mock<IPageService>();
            
        }
        [Fact]
        public void LoginUserShouldBeCalled()
        {
            // **** Arrange **** (Given)
            var vm = CreateInstance();
            vm.EmailEntry = "email?";

            // ***** Act ***** (When)
            vm.LoginCommand.Execute(null);

            // ***** Assert ***** (Then)
            _userService.Verify(x => x.LoginUser("email?"), Times.Once);
            _userService.VerifyNoOtherCalls();
        }

        [Fact]
        public void DisplayAlertShouldBeCalledForNotFoundUser()
        {
            // **** Arrange **** (Given)
            var vm = CreateInstance();
            vm.EmailEntry = "not@found.user";
            _userService.Setup(x => x.LoginUser("not@found.user")).Returns((User)null).Verifiable();

            // ***** Act ***** (When)
            vm.LoginCommand.Execute(null);

            // ***** Assert ***** (Then)
            _userService.Verify();
            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), "Invalid Credentials", It.IsAny<string>()));
            _pageService.VerifyNoOtherCalls();

        }

        [Fact]
        public void DisplayAlertShouldBeCalledForInvalidPassword()
        {
            // **** Arrange **** (Given)
            var vm = CreateInstance();
            vm.EmailEntry = "test@email.ru";
            vm.PasswordEntry = "password";
            _userService.Setup(x => x.LoginUser(It.IsAny<string>())).Returns(new User(vm.EmailEntry, "ppppp")).Verifiable();

            // ***** Act ***** (When)
            vm.LoginCommand.Execute(null);

            // ***** Assert ***** (Then)
            _userService.Verify();
            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), "Invalid Credentials", It.IsAny<string>()));
            _pageService.VerifyNoOtherCalls();
        }

        [Fact]
        public void DisplayAlertShouldBeCalledForInvalidPassword2()
        {
            // **** Arrange **** (Given)
            var vm = CreateInstance();
            vm.EmailEntry = "test@email.ru";
            vm.PasswordEntry = "password";
            User user = new User(vm.EmailEntry, vm.PasswordEntry);
            _userService.Setup(x => x.LoginUser(It.IsAny<string>())).Returns(user).Verifiable();

            // ***** Act ***** (When)
            vm.LoginCommand.Execute(null);

            // ***** Assert ***** (Then)
            _userService.Verify();
            _pageService.Verify(x => x.PushAsync(It.Is<HomePage>(y => y.User == user)));
            _pageService.VerifyNoOtherCalls();
            vm.PasswordEntry.ShouldBeNullOrEmpty();

        }

        private SignInViewModel CreateInstance()
        {
            return new SignInViewModel(_userService.Object, _pageService.Object);
        }
    }
}