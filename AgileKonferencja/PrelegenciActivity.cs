using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;

namespace AgileKonferencja
{
    [Activity(Label = "Prelegenci", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class PrelegenciActivity : Activity
    {
        // parametry połączenia z bazą danych
        MySqlTools myConnection = new MySqlTools("Server=188.128.164.101;Port=3306;database=14055177_0000006;User Id=14055177_0000006;Password=@Licja270778;charset=utf8");

        List<Prelegent> _prelegenci; // lista prelegentów

        int _currentSpeaker; // wskaźnik wyświetlanego aktualnie prelegenta

        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Prelegenci);

            _prelegenci = myConnection.GetSpeakerInfo(); // przypisz do listy prelegentów warość z bazy

            // pobranie i przypisanie zdjęcia do obiektu imageView
            ImageView imageViewPrelegenci = FindViewById<ImageView>(Resource.Id.ImageViewPrelegenci);
            Koush.UrlImageViewHelper.SetUrlDrawable(imageViewPrelegenci, _prelegenci.ElementAt(_currentSpeaker).ZdjeciePrelegenta); 
            imageViewPrelegenci.Click += ImageViewPrelegenci_Click; // przypisanie obsługi kliknięcia na zdjęciu prelegenta

            // przypisanie nazwy prelgenta z bazy danych
            TextView textViewNazwa = FindViewById<TextView>(Resource.Id.TextViewNazwa);
            textViewNazwa.Text = _prelegenci.ElementAt(_currentSpeaker).NazwaPrelegenta;

            // przypisanie opisu prelgenta
            TextView textViewPrelegenci = FindViewById<TextView>(Resource.Id.TextViewPrelegenci);
            textViewPrelegenci.Text = _prelegenci.ElementAt(_currentSpeaker).OpisPrelegenta;
            

        }

        /// <summary>
        /// metoda do obsługi kliknięcia w zdjęcie prelegenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageViewPrelegenci_Click(object sender, EventArgs e)
        {
            // jeśli lista dojdzie do końca zacznij od początku
            if (_currentSpeaker < myConnection.GetSpeakerCount()-1)
            {
                _currentSpeaker++;
            }
            else
            {
                _currentSpeaker = 0;
            }

            // podmiana zdjęcia na kolejne
            ImageView imageViewPrelegenci = FindViewById<ImageView>(Resource.Id.ImageViewPrelegenci);
            Koush.UrlImageViewHelper.SetUrlDrawable(imageViewPrelegenci, _prelegenci.ElementAt(_currentSpeaker).ZdjeciePrelegenta);

            // podmiana nazwy prelgenta na kolejne
            TextView textViewNazwa = FindViewById<TextView>(Resource.Id.TextViewNazwa);
            textViewNazwa.Text = _prelegenci.ElementAt(_currentSpeaker).NazwaPrelegenta;

            // podmiana opisu prelegenta na kolejnego
            TextView textViewPrelegenci = FindViewById<TextView>(Resource.Id.TextViewPrelegenci);
            textViewPrelegenci.Text = _prelegenci.ElementAt(_currentSpeaker).OpisPrelegenta;
        }
    }
}