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
    public class CreateAddressActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_create);
            var city = FindViewById<EditText>(Resource.Id.city);
            var pcode = FindViewById<EditText>(Resource.Id.pcode);
            var pid = FindViewById<EditText>(Resource.Id.pid);


            var cancel = FindViewById<Button>(Resource.Id.cancel);
            var add = FindViewById<Button>(Resource.Id.add);


            pid.Text = Intent.GetStringExtra("Id");
          

            add.Click += delegate
            {
                if (!string.IsNullOrEmpty(city.Text) && string.IsNullOrEmpty(pcode.Text)
                 && string.IsNullOrEmpty(pid.Text))
                {
                    var address = new SQLiteOperations.Address()
                    {
                        City = city.Text,
                        PCode = pcode.Text,
                        Id = Convert.ToInt16(pid.Text),                       
                    };
                    var sq = new SQLiteOperations();
                    sq.InsertAddress(address);

                    Intent i = new Intent(this, typeof(MainActivity));
                    StartActivity(i);
                }
                else
                {
                    Toast.MakeText(this, " City or Post Code or Person Id is empty", ToastLength.Short).Show();
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