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
    [Activity(Label = "ShowActivity")]
    public class ShowActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.activity_show);

            var user = FindViewById<TextView>(Resource.Id.user);
            var uid = FindViewById<TextView>(Resource.Id.uid);
            var code = FindViewById<TextView>(Resource.Id.code);

            var aid = FindViewById<TextView>(Resource.Id.aid);
            var city = FindViewById<TextView>(Resource.Id.city);
            var pcode = FindViewById<TextView>(Resource.Id.pcode);

            var showall = FindViewById<TextView>(Resource.Id.showall);


            var control = FindViewById<Button>(Resource.Id.control);
            var logout = FindViewById<Button>(Resource.Id.logout);
            var all = FindViewById<Button>(Resource.Id.all);
            var allbyaddress = FindViewById<Button>(Resource.Id.allbyaddress);


            string id = Intent.GetStringExtra("Id");
            var sq = new SQLiteOperations();
            var person = sq.GetPersonById(Convert.ToInt32(id));

            if(person != null)
            {
                uid.Text = person.Id.ToString();
                user.Text = person.User ;
                code.Text = person.Code;


                var address = sq.GetAddressByPersonId(person.Id);
                try
                {
                    aid.Text = address.AId.ToString();
                    city.Text = address.City;
                    pcode.Text = address.PCode;
                }catch(Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();

                }

            }


            control.Click += delegate
            {
                Intent i = new Intent(this, typeof(ControlActivity));
                i.PutExtra("Id", person.Id + "");
                StartActivity(i);
            };
            logout.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            };

            all.Click += delegate
            {
                var sq = new SQLiteOperations();
                var person = sq.GetAllPerson();
                string s = "";
                foreach(var p in person)
                {
                    s += p.Id + "   " + p.User + "   " + p.Code + "\n";
                }
                showall.Text = s;
            };


            allbyaddress.Click += delegate
            {
                var sq = new SQLiteOperations();
                var area = sq.GetAllPersonByaddress(city.Text, pcode.Text);
                string s = "";
                foreach (var p in area)
                {
                    var pers = sq.GetPersonById(p.Id);

                    s += pers.Id + "   " + pers.User + "   " + pers.Code + "\n";
                }
                showall.Text = s;
            };
        }
    }
}
