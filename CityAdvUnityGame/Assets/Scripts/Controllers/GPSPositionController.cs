using UnityEngine;
using System.Collections;

public class GPSPositionController : PositionController {
	
	private const float desiredAccuracy = 1.0f;
	private const float updateDistance = 1.0f;
	
	private Vector3? absolutePosition;
	
	private GPSBeaconManager beaconManager;

    private LocationServiceStatus prevStatus;
    private LocationInfo prevLocation;
	
	public GPSPositionController(PlayerManager playerManager)
		: base(playerManager)
	{
	}
	
	public override void Start ()
	{
		base.Start ();
		
		Debug.Log("C#: AutoPositionController Start");
        beaconManager = new GPSBeaconManager();
        
        if (Application.platform == RuntimePlatform.Android)
        {
            iPhoneSettings.StartLocationServiceUpdates(desiredAccuracy, updateDistance);
            //locationManager = new UnityLocationManager();
            //locationManager.onResume();
        }
	}
	
	public override void OnApplicationPause (bool pause)
	{
		base.OnApplicationPause (pause);
		
		Debug.Log("C#: AutoPositionController OnPuase: " + pause.ToString());
		
        if (beaconManager == null)	//not started
        {
            return;
        }

        if (pause)
        {
            //locationManager.onPause();
            iPhoneSettings.StopLocationServiceUpdates();
        }
        else
        {
            //locationManager.onResume();
            iPhoneSettings.StartLocationServiceUpdates(desiredAccuracy, updateDistance);
        }
	}
	
	public override void Update ()
	{
		
		 /*
            double latitude = locationManager.GetLatitude();
            double longitude = locationManager.GetLongitude();

            if (latitude != 0 && longitude != 0)
            {
                transform.position = beaconManager.GetPosition(latitude, longitude);
            }*/

        LocationServiceStatus status = iPhoneSettings.locationServiceStatus;

        if (status != LocationServiceStatus.Running)
        {
            if (prevStatus != status)
            {
                Debug.Log("Location Status: " + status.ToString());
                prevStatus = status;
            }
			absolutePosition = null;
        }
        else
        {
            LocationInfo loc = iPhoneInput.lastLocation;

            if (prevLocation.latitude != loc.latitude || prevLocation.longitude != loc.longitude)
            {
                Debug.Log("Location: " + loc.latitude.ToString() + ", " + loc.longitude.ToString());
                prevLocation = loc;
            }

            absolutePosition = beaconManager.GetPosition(loc.latitude, loc.longitude);
            absolutePosition = new Vector3(absolutePosition.Value.x, playerManager.transform.position.y, absolutePosition.Value.z);
        }
		
		base.Update ();
	}
	
	protected override Vector3? AbsolutePosition {
		get 
		{
			return absolutePosition;
		}
	}
}
