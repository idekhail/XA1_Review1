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

            var uid = FindViewById<TextView>(Resource.Id.uid);
            var user = FindViewById<EditText>(Resource.Id.user);
            var code = FindViewById<EditText>(Resource.Id.code);
           
            
            var aid = FindViewById<TextView>(Resource.Id.aid);
            var city = FindViewById<EditText>(Resource.Id.city);
            var pcode = FindViewById<EditText>(Resource.Id.pcode);


            var update = FindViewById<Button>(Resource.Id.update);
            var delete = FindViewById<Button>(Resource.Id.delete);
            var back = FindViewById<Button>(Resource.Id.back);
            var logout = FindViewById<Button>(Resource.Id.logout);


            string id = Intent.GetStringExtra("Id");
            var sq = new SQLiteOperations();

            var person = sq.GetPersonById(Convert.ToInt32(id));
            var address = sq.GetAddressByPersonId(Convert.ToInt32(id));

            uid.Text = person.Id.ToString();
            user.Text = person.User;
            code.Text = person.Code;

            aid.Text = address.AId.ToString();
            city.Text = address.City;
            pcode.Text = address.PCode;

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
                    Toast.MakeText(this, " Person is updated", ToastLength.Short).Show();

                }
                else
                {
                    Toast.MakeText(this, " User or code is empty", ToastLength.Short).Show();
                }
                //  Address Update
                if (city.Text != "" && pcode.Text != "")
                {
                    address.City = city.Text;
                    address.PCode = pcode.Text;

                    sq.UpdateAddress(address);
                    Toast.MakeText(this, " address is updated", ToastLength.Short).Show();

                }
                else
                {
                    Toast.MakeText(this, " City or pcode is empty", ToastLength.Short).Show();
                }
            };
           
            delete.Click += delegate
            {
                sq.DeletePerson(person);
                sq.DeleteAddress(address);
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            };
        }
    }
}