using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WASMAspHosted.Client.Infrastructure
{
    public class MyAuthProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        public MyAuthProvider(IJSRuntime js)
        {
            _js = js;
        }

        public string token { get; set; }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(2000);
            //ClaimsIdentity anonymousUser = new ClaimsIdentity();

            //return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymousUser)));

            //List<Claim> claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Role, "user"),
            //    new Claim(ClaimTypes.Name, "Jean-michel")
            //};

            //ClaimsIdentity myUser = new ClaimsIdentity(claims, "TestAuth");
            //return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(myUser)));

            List<Claim> claims = new List<Claim>();
            token = await _js.InvokeAsync<string>("localStorage.getItem", "token");

            if (string.IsNullOrEmpty(token))
            {
                ClaimsIdentity anonymous = new ClaimsIdentity();
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
            }

            JwtSecurityToken jwt = new JwtSecurityToken(token);

            foreach(Claim claim in jwt.Claims)
            {
                claims.Add(claim);
            }

            ClaimsIdentity currentUser = new ClaimsIdentity(claims, "AuthPolicy");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(currentUser)));
        }

        public void NotifyUserChanged()
        {
            Console.WriteLine("Notify");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
