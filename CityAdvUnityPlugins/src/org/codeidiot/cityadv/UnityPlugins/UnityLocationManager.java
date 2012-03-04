package org.codeidiot.cityadv.UnityPlugins;

import android.content.Context;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.util.Log;

public class UnityLocationManager {
	private static final String TAG = "UnityLocationManager";
	
	private static final int MIN_TIME = 1000;	//1s
	private static final int MIN_DISTANCE = 5;	//5m
	
	private LocationManager locationManager;
	private Criteria criteria;
	
	private Location currentLocation;
	
	public UnityLocationManager(Context context) {
		Log.d(TAG, "Creating location manager...");
		
		locationManager = (LocationManager)context.getSystemService(Context.LOCATION_SERVICE);
		for(String provider : locationManager.getProviders(true)) {
			Log.d(TAG, "Provider: " + provider);
		}	
		
	    criteria = new Criteria();
	    criteria.setAccuracy(Criteria.ACCURACY_FINE);
	    criteria.setAltitudeRequired(false);
	    criteria.setBearingRequired(false);
	    criteria.setCostAllowed(true);
	    criteria.setPowerRequirement(Criteria.POWER_HIGH);
	    
	    Log.d(TAG, "Creating location manager finished");
	}
	
	public void onResume() {
		Log.d(TAG, "location onResume");
	    String provider = locationManager.getBestProvider(criteria, true);
	    Log.d(TAG, "Best provider: " + provider);
	    currentLocation = locationManager.getLastKnownLocation(provider);
	    Log.d(TAG, "Last loc: " + currentLocation.getLatitude() + ", " + currentLocation.getLongitude());
	    
	    locationManager.requestLocationUpdates(provider, MIN_TIME, MIN_DISTANCE, locationListener);
	    Log.d(TAG, "Location listening");
	}
	
	public void onPause() {
		Log.d(TAG, "location onPause");
		locationManager.removeUpdates(locationListener);
		Log.d(TAG, "Location listening stopped");
	}
	
	public double getLatitude() {
		Log.d(TAG, "location getLatitude");
		return currentLocation == null ? 0 : currentLocation.getLatitude();
	}
	
	public double getLongitude() {
		Log.d(TAG, "location getLongitude");
		return currentLocation == null ? 0 : currentLocation.getLongitude();
	}
	
	private LocationListener locationListener = new LocationListener() {
		
		@Override
		public void onStatusChanged(String provider, int status, Bundle extras) {
			// TODO Auto-generated method stub
			
		}
		
		@Override
		public void onProviderEnabled(String provider) {
			// TODO Auto-generated method stub
			
		}
		
		@Override
		public void onProviderDisabled(String provider) {
			// TODO Auto-generated method stub
			
		}
		
		@Override
		public void onLocationChanged(Location location) {
			Log.d(TAG, "Location: (" + location.getLatitude() + ", " + location.getLongitude() + ")");
			currentLocation = location;		
		}
	};
}
