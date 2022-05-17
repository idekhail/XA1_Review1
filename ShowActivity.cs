﻿using Android.App;
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

            var user = FindViewById<EditText>(Resource.Id.user);
            var code = FindViewById<TextView>(Resource.Id.code);
            var showall = FindViewById<TextView>(Resource.Id.showall);


            var control = FindViewById<Button>(Resource.Id.control);
            var logout = FindViewById<Button>(Resource.Id.logout);
            var all = FindViewById<Button>(Resource.Id.all);
            var allperson = FindViewById<Button>(Resource.Id.allperson);


            string id = Intent.GetStringExtra("Id");
            var sq = new SQLiteOperations();
            var person = sq.GetPersonById(Convert.ToInt32(id));

            if(person != null)
            {
                user.Text = person.User;
                code.Text = person.Code;

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
            allperson.Click += delegate
            {
                var sq = new SQLiteOperations();
                var person = sq.GetPersonByUser(user.Text);
                string s = "";
                foreach (var p in person)
                {
                    s += p.Id + "   " + p.User + "   " + p.Code + "\n";
                }
                showall.Text = s;
            };
        }
    }
}