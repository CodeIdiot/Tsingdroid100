using UnityEngine;
using System.Collections;

public class AndroidAccelOrientationManager : OrientationController {

	private UnitySensorManager sensorManager;
    //private const float updateInterval = 1.0f / 60.0f;
    //private const float lowPassKernelWidth = 1.0f;
    //private const float lowPassFactor = updateInterval / lowPassKernelWidth;
	
	public AndroidAccelOrientationManager(PlayerManager playerManager)
		:base(playerManager)
	{
	}
	
	public override void Start ()
	{
		base.Start();
		sensorManager = new UnitySensorManager();
        sensorManager.onResume();
	}
	
	public override void OnApplicationPause (bool pause)
	{
		base.OnApplicationPause (pause);
	    if (sensorManager == null)
	    {
	        return;
	    }
	
	    if (pause)
	    {
	        sensorManager.onPause();
	    }
	    else
	    {
	        sensorManager.onResume();
	    }
	}
	
	protected override Vector3? EulerAngles {
		get 
		{
			if(sensorManager != null)
			{
				return new Vector3(-sensorManager.getXAngle(), -sensorManager.getYAngle(), 0);
			}
			else
			{
				return null;
			}
		}
	}
}
