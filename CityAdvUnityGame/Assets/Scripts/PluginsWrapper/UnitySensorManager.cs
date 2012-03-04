using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
public class UnitySensorManager
{
    private AndroidJavaObject currentActivity;
    private AndroidJavaObject sensorManager;

    public UnitySensorManager()
    {
        Debug.Log("C#: Creating sensor manager...");

        //find current activity
        currentActivity = PluginsHelper.GetUnityActivity();

        //create new sensor manager
        sensorManager = PluginsHelper.CreatePluginObject("UnitySensorManager", currentActivity);
    }

    public float getXAngle()
    {
        return sensorManager.Call<float>("getXAngle");
    }

    public float getYAngle()
    {
        return sensorManager.Call<float>("getYAngle");
    }

    public float getZAngle()
    {
        return sensorManager.Call<float>("getZAngle");
    }

    public void onResume()
    {
        sensorManager.Call("onResume");
    }

    public void onPause()
    {
        sensorManager.Call("onPause");
    }
}
#endif
