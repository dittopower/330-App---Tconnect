using System; 
using Android.App;
using Tconnect;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Tconnect.Droid;
using System.Diagnostics;

[assembly: ExportRenderer (typeof (LoginPage), typeof (LoginPageRenderer))]

namespace Tconnect.Droid
{
	public class LoginPageRenderer : PageRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			// this is a ViewGroup - so should be able to load an AXML file and FindView<>
			var activity = this.Context as Activity;

			var auth = new OAuth2Authenticator (
				clientId: "pXQhRdqeGPraG8z85f139g", // your OAuth2 client id
				scope: "", // The scopes for the particular API you're accessing. The format for this will vary by API.
				authorizeUrl: new Uri ("https://www.yammer.com/dialog/oauth"), // the auth URL for the service
				redirectUrl: new Uri ("http://deamon.info/university/330/yammer.php")); // the redirect URL for the service

			auth.Completed += (sender, eventArgs) => {
				//Console.WriteLine("hi");
				//Console.WriteLine(eventArgs);
				if (eventArgs.IsAuthenticated) {
					App.SuccessfulLoginAction.Invoke();
					Console.WriteLine("yammer connected");
					Console.WriteLine(eventArgs.Account);
					// Use eventArgs.Account to do wonderful things
					App.SaveToken(eventArgs.Account.Properties["access_token"]);
				} else {
					// The user cancelled
					Console.WriteLine("yammer not connected");
				}
				//Console.WriteLine("bye");
				//Console.WriteLine(App.Token);
			};

			activity.StartActivity (auth.GetUI(activity));
		}
	}
}