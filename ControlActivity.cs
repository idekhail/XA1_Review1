using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XA1_Review1
{
    [Activity(Label = "ControlActivity")]
    public class ControlActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_control);

            var user = FindViewById<EditText>(Resource.Id.user);
            var code = FindViewById<EditText>(Resource.Id.code);

            var update = FindViewById<Button>(Resource.Id.update);
            var delete = FindViewById<Button>(Resource.Id.delete);
            var back = FindViewById<Button>(Resource.Id.back);
            var logout = FindViewById<Button>(Resource.Id.logout);


            string id = Intent.GetStringExtra("Id");
            var sq = new SQLiteOperations();
            var person = sq.GetPersonById(Convert.ToInt32(id));

            user.Text = person.User;
            code.Text = person.Code;
           
            back.Click += delegate
            {
                Intent i = new Intent(this, typeof(ShowActivity));
                i.PutExtra("Id", person.Id + "");
                StartActivity(i);
            };
            logout.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            };

            update.Click += delegate
            {
                if (user.Text != "" && code.Text != "")
                {

                    person.User = user.Text;
                    person.Code = code.Text;
                   
                    sq.UpdatePerson(person);
                    Intent i = new Intent(this, typeof(ShowActivity));
                    i.PutExtra("Id", person.Id.ToString());
                    StartActivity(i);
                }
                else
                {
                    Toast.MakeText(this, " User or code is empty", ToastLength.Short).Show();
                }
            };
           
            delete.Click += delegate
            {
                sq.DeletePerson(person);
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            };
        }
    }
}