using UnityEngine;
using System.Collections;

public class GPSBeaconManager {

    private GPSBeaconComponent beaconNw, beaconSe;
    private Vector3 beaconNwPos, beaconSePos;

    public GPSBeaconManager()
    {
        //build beacon index
        GameObject objNw = GameObject.Find("GPSBeaconNW");
        GameObject objSe = GameObject.Find("GPSBeaconSE");

        beaconNw = objNw.GetComponent<GPSBeaconComponent>();
        beaconSe = objSe.GetComponent<GPSBeaconComponent>();

        beaconNwPos = objNw.transform.position;
        beaconSePos = objSe.transform.position;
    }

    public Vector3 GetPosition(double latitude, double longitude)
    {
        Vector3 result = new Vector3();
        result.x = (float)((latitude - beaconNw.Latitude) / (beaconSe.Latitude - beaconNw.Latitude) * (beaconSePos.x - beaconNwPos.x) + beaconNwPos.x);
        result.z = (float)((longitude - beaconNw.Longitude) / (beaconSe.Longitude - beaconNw.Longitude) * (beaconSePos.z - beaconNwPos.z) + beaconNwPos.z);
        result.y = 0;   //TODO: altitude?

        return result;
    }
	
	public void GetGpsPosition(Vector3 position, out double latitude, out double longitude) {
		latitude = (position.x - beaconNwPos.x) / (beaconSePos.x - beaconNwPos.x) * (beaconSe.Latitude - beaconNw.Latitude) + beaconNw.Latitude;
		longitude = (position.z - beaconNwPos.z) / (beaconSePos.z - beaconNwPos.z) * (beaconSe.Longitude - beaconNw.Longitude) + beaconNw.Longitude;
	}
}
