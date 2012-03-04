using UnityEngine;
using System.Collections;

public class GPSBeaconComponent : MonoBehaviour {
    public int latitude_degree;
    public int latitude_minute;
    public double latitude_second;
    public int longitude_degree;
    public int longitude_minute;
    public double longitude_second;

    public double Latitude
    {
        get { return (double)latitude_degree + latitude_minute / 60.0 + latitude_second / 3600.0; }
    }

    public double Longitude
    {
        get { return (double)longitude_degree + longitude_minute / 60.0 + longitude_second / 3600.0; }
    }
}

