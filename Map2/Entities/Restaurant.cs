using Android.Gms.Maps.Model;

namespace Map2.Entities
{
    class Restaurant
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Schedule { get; set; }
        public LatLng Coordinates { get; set; }
    }
}