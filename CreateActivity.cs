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
    [Activity(Label = "CreateActivity")]
    public class CreateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_create);
            var user = FindViewById<EditText>(Resource.Id.user);
            var code = FindViewById<EditText>(Resource.Id.code);
           

            var cancel = FindViewById<Button>(Resource.Id.cancel);
            var add = FindViewById<Button>(Resource.Id.add);

            add.Click += delegate
            {
                if (user.Text != "" && code.Text != "")
                {
                    var person = new SQLiteOperations.Person()
                    {
                        User = user.Text,
                        Code = code.Text,
                       

                        //website = "edfh"
                    };
                    var sq = new SQLiteOperations();
                    sq.InsertPerson(person);

                    Intent i = new Intent(this, typeof(MainActivity));
                    StartActivity(i);
                }
                else
                {
                    Toast.MakeText(this, " UserName or Password is empty", ToastLength.Short).Show();
                }
            };
           
            cancel.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            };

        }
    }
}