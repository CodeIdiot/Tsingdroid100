using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
public class UnityLocationManager {

    private AndroidJavaObject currentActivity;
    private AndroidJavaObject locationManager;

    public UnityLocationManager()
    {
        Debug.Log("C#: Creating location manager...");

        //find current activity
        currentActivity = PluginsHelper.GetUnityActivity();

        //create new sensor manager
        locationManager = PluginsHelper.CreatePluginObject("UnityLocationManager", currentActivity);

        Debug.Log("C#: Creating location manager finished.");
    }

    public double GetLatitude()
    {
        Debug.Log("C#: getting latitude");
        return locationManager.Call<double>("getLatitude");
    }

    public double GetLongitude()
    {
        Debug.Log("C#: getting longitude");
        return locationManager.Call<double>("getLongitude");
    }

    public void onResume()
    {
        Debug.Log("C#: location onResume");
        locationManager.Call("onResume");
    }

    public void onPause()
    {
        Debug.Log("C#: location onPause");
        locationManager.Call("onPause");
    }
}
#endif
