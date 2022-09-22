# MAUI OidcClient demoeksempler

- Projektet: OidcClient-MauiApp2, som kan logge ind og tilgå Api 
- Projektet: MAUI-OidcClient, som indeholder en menu til Login, API og Logout

Benytter demo.duendesoftware.com. [Duende .well-known/openid-configuration](https://demo.duendesoftware.com/.well-known/openid-configuration)

## Beskrivelse
Demonstrerer den simpleste måde at benytte en authenticationserver til at lave login på en MAUI app vha. WebAuthenticator og OidcClient klassen.

Når brugeren er logget ind, vises en velkomst, ID Token, Access Token og brugerens claims Bemærk at alle data også udskrives til Output vinduet når
man kører i Debug mode. Det giver mulighed for at kopiere Access Token over i f.eks [jwt.io](jwt.io) for nærmere inspektion.


**Virker på Android:**
- Pixel 4 fysisk device (Android 13)
- Pø Android 12+ Emulator skal man i emulatoren vælge `Settings | Advanced` og sætte `OpenGL ES renderer = ANGLE (D3D11)`

**Virker på iPhone:** (Husk iOS Bundle Signing, Scheme = Automatic Provisioning og under Configure vælges Team)
- iOS Simulators (iPhone 13 Pro iOS 15.5) vises på Windows pc
- iPhone, fysisk device

**Virker ikke på Windows**
- Der ventes på en opdatering.

### Libraries

- [IdentityModel.OidcClient](https://github.com/IdentityModel/IdentityModel.OidcClient), version 5.0.2

### Configuration
Ved brug af demo.duendesoftware.com som identityprovider, anvendes følgende konfiguration i *MauiProgram.cs*:

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
Husk følgende tilføjelse til AndroidManifest ([Implementation guide](https://developer.chrome.com/docs/android/custom-tabs/integration-guide/))

```xml
<queries>
    <intent>
        <action android:name=
            "android.support.customtabs.action.CustomTabsService" />
    </intent>
</queries>
```
