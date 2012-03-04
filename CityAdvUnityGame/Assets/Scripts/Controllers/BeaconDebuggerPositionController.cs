using UnityEngine;
using System.Collections;

public class BeaconDebuggerPositionController : MonoBehaviour {

    public int latitude_degree;
    public int latitude_minute;
    public double latitude_second;
    public int longitude_degree;
    public int longitude_minute;
    public double longitude_second;

    public Vector3 position;

    private GPSBeaconManager beaconManager;

    void OnGUI()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return;
        }

        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 5, 5), "+");
    }

    // Use this for initialization
    void Start () {
        if (Application.platform == RuntimePlatform.Android)
        {
            return;
        }

        beaconManager = new GPSBeaconManager();
    }
    
    // Update is called once per frame
    void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            return;
        }

        position = beaconManager.GetPosition(
            (double)latitude_degree + latitude_minute / 60.0 + latitude_second / 3600.0, 
            (double)longitude_degree + longitude_minute / 60.0 + longitude_second / 3600.0);
        transform.position = new Vector3(position.x, transform.position.y, position.z);
    }
}
