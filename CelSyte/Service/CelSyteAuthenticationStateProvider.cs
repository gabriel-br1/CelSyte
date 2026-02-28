using CelSyte.Data;
using CelSyte.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
namespace CelSyte.Service
{
    public class CelSyteAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {

        private readonly UserService _userService;

        private readonly CelSyteContext _celSyteContext;

        public User CurrentUser { get; private set; } = new User();

        public CelSyteAuthenticationStateProvider(UserService userService, CelSyteContext context)
        {
            _userService = userService;
            _celSyteContext = context;
        }

        public async Task LoginAsync(string email, string password)
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();
            User? user = _userService.FindUserInDatabase(email, password, _celSyteContext);

            if(user != null)
            {
                await _userService.PersistUserToBrowserAsync(user);
                principal = user.ToClaimsPrincipal();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task LogoutAsync()
        {
            await _userService.ClearBrowserDataAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();
            User? user = await _userService.FetchUserFromBrowserAsync();

            if(user != null)
            {
                User? userInDatabase = _userService.FindUserInDatabase(user.Email, user.Password, _celSyteContext);

                if( userInDatabase != null)
                {
                    principal = userInDatabase.ToClaimsPrincipal();
                    CurrentUser = userInDatabase;
                }
            }

            return new(principal);
        }

        private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if(authenticationState != null)
            {
                CurrentUser = User.FromClaimsPrincipal(authenticationState.User);
            }
        }

        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;

    }
}
