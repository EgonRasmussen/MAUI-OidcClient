# MAUI-OidcClient
Benytter demo.duendesoftware.com. [Duende .well-known/openid-configuration](https://demo.duendesoftware.com/.well-known/openid-configuration)

Baserer sig p� f�lgende eksempel:
[Add Authentication to .NET MAUI Apps with Auth0](https://auth0.com/blog/add-authentication-to-dotnet-maui-apps-with-auth0/)


## Beskrivelse
Demonstrerer den simpleste m�de at benytte Auth0 til at lave login p� en MAUI app.

N�r brugeren er logget ind, vises en velkomst, ID Token, Access Token og brugerens claims Bem�rk at alle data ogs� udskrives til Output vinduet n�r
man k�rer i Debug mode. Det giver mulighed for at kopiere Access Token over i f.eks [jwt.io](jwt.io) for n�rmere inspektion.


**Virker p� Android:**
- Pixel 4 fysisk device (Android 13)
- P� Android 12+ Emulator skal man i emulatoren v�lge `Settings | Advanced` og s�tte `OpenGL ES renderer = ANGLE (D3D11)`

**Virker p� iPhone:** (Husk iOS Bundle Signing, Scheme = Automatic Provisioning og under Configure v�lges Team)
- iOS Simulators (iPhone 13 Pro iOS 15.5) vises p� Windows pc
- iPhone, fysisk device

**Virker ikke p� Windows**
- Der ventes p� en opdatering.

### Libraries

- [IdentityModel.OidcClient](https://github.com/IdentityModel/IdentityModel.OidcClient), version 5.0.2

### Configuration
Ved brug af demo.duendesoftware.com som identityprovider, anvendes f�lgende konfiguration i *MauiProgram.cs*:

```c#
var options = new OidcClientOptions
{
    Domain = "demo.duendesoftware.com",
    ClientId = "interactive.public",
    Scope = "openid profile email api offline_access",
    RedirectUri = "myapp://callback"
};
```
Ved Duende er `end_session_endpoint` hardkodet til: `connect/endsession`.

Ved Duende benyttes `Audience` ikke. Ved eget API skal Parameteren til `FrontChannelExtraParameters` s�ttes lig `Audience` for det benyttede API.

**Android 11+**
Husk f�lgende tilf�jelse til AndroidManifest ([Implementation guide](https://developer.chrome.com/docs/android/custom-tabs/integration-guide/))

```xml
<queries>
    <intent>
        <action android:name=
            "android.support.customtabs.action.CustomTabsService" />
    </intent>
</queries>
```
