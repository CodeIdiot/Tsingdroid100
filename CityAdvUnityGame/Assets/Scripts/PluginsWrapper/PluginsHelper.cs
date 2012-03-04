using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
public static class PluginsHelper  {
    public static readonly string UnityActivityClassName = "com.unity3d.player.UnityPlayer";
    public static readonly string PluginPackageName = "org.codeidiot.cityadv.UnityPlugins";

    public static AndroidJavaObject GetUnityActivity()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        return jo;
    }

    public static AndroidJavaClass GetPluginClass(string className)
    {
        AndroidJavaClass jc = new AndroidJavaClass(PluginPackageName + "." + className);
        return jc;
    }

    public static AndroidJavaObject CreatePluginObject(string className, params object[] args)
    {
        AndroidJavaObject jo = new AndroidJavaObject(PluginPackageName + "." + className, args);
        return jo;
    }
    
}
#endif