using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;

namespace AgileKonferencja
{
    [Activity(Label = "Ogarnij Agile!", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // utworzenie obsługi zdarzenia kliknięcia przycisku Połącz z organizatorami
            Button buttonConnect = FindViewById<Button>(Resource.Id.ButtonConnect);
            buttonConnect.Click += ButtonConnect_Click; // funkcja obsługi kliknięcia

            // zamiana istniejącego toolbara na ActionBar
            // ważne by utworzyć nowy własnu styl, w którym wyłączyć domyślny ActionBar
            // styl trzeba podpiąć w AndroidManifest.xml
            Toolbar toolbarMain = FindViewById<Toolbar>(Resource.Id.ToolbarMain);
            SetActionBar(toolbarMain);

            //Koush.UrlImageViewHelper.SetUrlDrawable(imageViewSponsorzy, "https://d1ll4kxfi4ofbm.cloudfront.net/file/event/144319/logo/logo3_144319_20170104121733.png");

        }

        // metoda obsługująca zdarzenie kliknięcia przycisku połączenia z organizatorami
        private void ButtonConnect_Click(object sender, System.EventArgs e)
        {
            var callIntent = new Intent(Intent.ActionCall);
            callIntent.SetData(Android.Net.Uri.Parse("tel:" + "664404411"));
            StartActivity(callIntent);
        }

        // metoda do podmiany obsługi menu i podpięcia własnego
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        //metoda do obsługi menu z ActionBar
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // dodatkowy dymek używany w celach testowych
            Toast.MakeText(this, item.TitleFormatted + " (" + item.ItemId + ")", ToastLength.Short).Show();

            // uruchamianie własciwego zdarzenia w zależności co jest wybrane z menu
            switch (item.TitleFormatted.ToString())
            {
                case "Wyjście":
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                    break;
                case "Agenda":
                    var intentAgenda = new Intent(this, typeof(AgendaActivity));
                    StartActivity(intentAgenda);
                    break;
                case "Prelegenci":
                    var intentPrelegenci = new Intent(this, typeof(PrelegenciActivity));
                    StartActivity(intentPrelegenci);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}

