using System.Collections.Generic;

namespace WindowsFormsApplication3
{
    public class V
    {

        public List<E> e = new List<E>();
        public double Longitude { get; set; }
        public double Lititude { get; set; }
        public int v { get; set; }

        public V(int v, double longitude, double lititude)
        {
            e = new List<E>();
            Longitude = longitude;
            Lititude = lititude;
            this.v = v;
        }

        public V()
        {
        }

        
    }
}
