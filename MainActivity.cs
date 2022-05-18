using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;

using AndroidX.AppCompat.App;

namespace XA1_Review1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var user = FindViewById<EditText>(Resource.Id.user);
            var code = FindViewById<EditText>(Resource.Id.code);
            var ok = FindViewById<Button>(Resource.Id.ok);
            var createperson = FindViewById<Button>(Resource.Id.createperson);
           
            var createaddress = FindViewById<Button>(Resource.Id.createaddress);
            var close = FindViewById<Button>(Resource.Id.close);


            ok.Click += delegate
            {
                if (!string.IsNullOrEmpty(user.Text) && !string.IsNullOrEmpty(code.Text))
                {
                    var sq = new SQLiteOperations();
                    var person = sq.GetPerson(user.Text, code.Text);
                    if (person != null)
                    {
                        Intent i = new Intent(this, typeof(ShowActivity));
                        i.PutExtra("Id", person.Id.ToString());
                        StartActivity(i);
                    }
                    else
                        Toast.MakeText(this, "user or code is not Found", ToastLength.Long).Show();
                }
                else
                    Toast.MakeText(this, "user or code is empty !!!!", ToastLength.Long).Show();
            };

            createperson.Click += delegate
            {
                Intent i = new Intent(this, typeof(CreateActivity));
                StartActivity(i);
            };

            createaddress.Click += delegate
            {

                if (!string.IsNullOrEmpty(user.Text) && !string.IsNullOrEmpty(code.Text))
                {
                    var sq = new SQLiteOperations();
                    var person = sq.GetPerson(user.Text, code.Text);
                    if (person != null)
                    {
                        Intent i = new Intent(this, typeof(CreateAddressActivity));
                        i.PutExtra("Id", person.Id.ToString());
                        StartActivity(i);
                    }
                    else
                        Toast.MakeText(this, "user or code is not Found", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "خطاء", ToastLength.Long).Show();

                    //Intent i = new Intent(this, typeof(CreateAddressActivity));
                    //StartActivity(i);
                }

            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}