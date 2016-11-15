using System.Collections.Generic;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Android.Widget;
using Map2.Entities;

namespace Map2
{
    [Activity(Label = "McDonaldsMap2", MainLauncher = true, Icon = "@drawable/icon")]
    public class MapActivity : Activity, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter, GoogleMap.IOnInfoWindowClickListener
    {
        private GoogleMap _map;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            CreateMap();
        }

        private void CreateMap()
        {
            if (_map == null)
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

            _map.MyLocationEnabled = true;
            _map.MyLocationChange += _map_MyLocationChange;

            this.InitializeMap();
        }

        private void _map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(new LatLng(e.Location.Latitude, e.Location.Longitude), 12);
            _map.AnimateCamera(cameraUpdate);
        }

        private void InitializeMap()
        {
            var restaurants = new List<Restaurant>
            {
                #region Initializing
                
                new Restaurant()
                {
                    Id=1,
                    Adress = "Bla Str.",
                    Schedule = "8:00-18:00",
                    Coordinates=new LatLng(50.492340, 30.470238)
                },
                new Restaurant()
                {
                    Id=2,
                    Adress = "Bla-bla Str.",
                    Schedule = "8:00-18:00",
                    Coordinates=new LatLng(50.484040, 30.457878)
                },
                new Restaurant()
                {
                    Id=3,
                    Adress = "Blaaa Str.",
                    Schedule = "8:00-18:00",
                    Coordinates=new LatLng(50.467435, 30.477104)
                },
                new Restaurant()
                {
                    Id=4,
                    Adress = "La Str.",
                    Schedule = "8:00-18:00",
                    Coordinates=new LatLng(50.466560, 30.497017)
                },
                new Restaurant()
                {
                    Id=5,
                    Adress = "Bala Str.",
                    Schedule = "8:00-18:00",
                    Coordinates=new LatLng(50.486661, 30.494957)
                },
                new Restaurant()
                {
                    Id=6,
                    Adress = "Blaaa-bla Str.",
                    Schedule = "8:00-18:00",
                    Coordinates=new LatLng(50.479234, 30.486031)
                },
                new Restaurant()
                {
                    Id=7,
                    Adress = "Bla Str.",
                    Schedule = "8:00-18:00",
                    Coordinates=new LatLng(50.467435, 30.508003)
                }
               #endregion
            };

            foreach (var restaurant in restaurants)
            {
                var markerOptions = new MarkerOptions()
                    .SetPosition(restaurant.Coordinates)
                    .SetSnippet(restaurant.Adress)
                    .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.m_marker));

                _map.AddMarker(markerOptions);
            }

            // Camera focusing
            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(restaurants[3].Coordinates, 12);
            _map.MoveCamera(cameraUpdate);

            _map.SetInfoWindowAdapter(this);
            _map.SetOnInfoWindowClickListener(this);
        }

        public View GetInfoContents(Marker marker)
        {
            return null;
        }

        public View GetInfoWindow(Marker marker)
        {
            var view = LayoutInflater.Inflate(Resource.Layout.InfoWindow, null, false);
            view.FindViewById<TextView>(Resource.Id.adressTxtBox).Text = marker.Snippet;
            return view;
        }

        public void OnInfoWindowClick(Marker marker)
        {
            new AlertDialog.Builder(this)
                .SetPositiveButton("Yes", (sender, args) =>
                {
                    // Переход в меню
                })
                .SetNegativeButton("No", (sender, args) => { })
                .SetMessage($"Переход в меню ресторана:\n{marker.Snippet}") // Address
                .SetTitle("Подтверждение")
                .Show();
        }
    }
}

