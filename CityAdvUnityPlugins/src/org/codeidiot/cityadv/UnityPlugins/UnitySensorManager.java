package org.codeidiot.cityadv.UnityPlugins;

import android.app.Activity;
import android.hardware.Sensor;
import android.hardware.SensorManager;
import android.util.Log;

public class UnitySensorManager {
	private static final String TAG = "UnitySensorManager";
	
	private SensorManager sensorManager;
	private Sensor accSensor;
	private Sensor orientSensor;
	
	private SensorCameraController cameraController;
	
	private Activity activity;
	
	public UnitySensorManager(Activity activity) {
		
		Log.d(TAG, "Creating UnitySensorManager...");
		this.activity = activity;
		
		sensorManager = (SensorManager) activity.getSystemService(Activity.SENSOR_SERVICE);
		accSensor = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
		orientSensor = sensorManager.getDefaultSensor(Sensor.TYPE_ORIENTATION);

		cameraController = new SensorCameraController(accSensor, orientSensor, activity.getWindowManager().getDefaultDisplay());
	}
	
	public void onResume() {
		sensorManager.registerListener(cameraController, accSensor, SensorManager.SENSOR_DELAY_GAME);
		sensorManager.registerListener(cameraController, orientSensor, SensorManager.SENSOR_DELAY_GAME);
		cameraController.onResume();
		Log.d(TAG, "Sensor listening");
	}
	
	public void onPause() {
		sensorManager.unregisterListener(cameraController);
		cameraController.onPause();
		Log.d(TAG, "Sensor listenning stopped");
	}
	
	public float getXAngle() {
		return cameraController.getXAngle();
	}

	public float getYAngle() {
		return cameraController.getYAngle();
	}

	public float getZAngle() {
		return cameraController.getZAngle();
	}
}
