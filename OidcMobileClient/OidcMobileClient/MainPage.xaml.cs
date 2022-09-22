using OidcMobileClient.Auth0;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OidcMobileClient;

public partial class MainPage : ContentPage
{
    HttpClient apiClient;
    private readonly Auth0Client auth0Client;

    public MainPage(Auth0Client client)
    {
        InitializeComponent();
        auth0Client = client;
        apiClient = new HttpClient() { BaseAddress = new Uri("https://demo.duendesoftware.com/") };
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginResult = await auth0Client.LoginAsync();

        if (loginResult.IsError)
        {
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }

        var sb = new StringBuilder(128);
        foreach (var claim in loginResult.User.Claims)
        {
            sb.AppendFormat("{0}: {1}\n", claim.Type, claim.Value);
        }

        sb.AppendFormat("\n{0}: {1}\n", "refresh token", loginResult?.RefreshToken ?? "none");
        sb.AppendFormat("\n{0}: {1}\n", "access token", loginResult.AccessToken);

        OutputText.Text = sb.ToString();

        System.Diagnostics.Debug.WriteLine(sb.ToString());

        apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult?.AccessToken ?? "");
        HomeLayout.IsVisible = false; OidcDataLayout.IsVisible = true;
    }

    private async void OnCallAPIClicked(object sender, EventArgs e)
    {
        var result = await apiClient.GetAsync("api/test");

        if (result.IsSuccessStatusCode)
        {
            OutputText.Text = JsonDocument.Parse(await result.Content.ReadAsStringAsync()).RootElement.GetRawText();
        }
        else
        {
            OutputText.Text = result.ReasonPhrase;
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var logoutResult = await auth0Client.LogoutAsync();

        if (!logoutResult.IsError)
        {
            HomeLayout.IsVisible = true;
            OidcDataLayout.IsVisible = false;
        }
        else
        {
            await DisplayAlert("Error", logoutResult.ErrorDescription, "OK");
        }
    }
}

