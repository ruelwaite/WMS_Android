using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WMS_Android.Classes.Activities;
using WMS_Android.Classes;
using WMS_Android.Classes.Model;
using System.Linq;

namespace WMS_Android.Activities
{
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);



            var button = FindViewById<Button>(Resource.Id.btnLogin);

            button.Click += (sender, e) =>
            {
                var lstUsers = Globals.GetDB().Table<User>().ToList();
                var txtUsername = FindViewById<TextView>(Resource.Id.txtUsername);
                var txtPassword = FindViewById<TextView>(Resource.Id.txtPassword);
                var txtLoginMessage = FindViewById<TextView>(Resource.Id.txtLoginMessage);

                if (lstUsers.Where(s=> s.Username == txtUsername.Text && string.Compare(s.Password, txtPassword.Text, false)==0).ToList().Count() == 0)
                {
                    txtLoginMessage.Text = GetString(Resource.String.IncorrectUserPassword);
                    return;
                }

                var homeActivity = new Intent(this, typeof(HomeActivity));
                StartActivity(homeActivity);
            };

        }
    }
}