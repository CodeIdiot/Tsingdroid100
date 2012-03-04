package org.codeidiot.cityadv.UnityPlugins;

import java.io.BufferedWriter;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.util.Log;
import android.view.Display;

class SensorCameraController implements SensorEventListener {

	private static final String TAG = "SensorCameraController";
	private static final String LOG_PATH = "/sdcard/tsingdroid_log/";

	// setup default value
	private float XAngle = 0;
	private float YAngle = 0;
	private float ZAngle = 0;

	private int orientation = 1;

	private Sensor accSensor;
	private Sensor orientSensor;
	private float[] Rotation = new float[9];

	private float[] angleInRadian = new float[3];
	private float[] geomagnetic = new float[] { 0, 1.0f, 0 };

	private Display display = null;

	private double[] x1, y1;
	
	private BufferedWriter logWriter;

	public SensorCameraController(Sensor acc, Sensor orient, Display display) {
		accSensor = acc;
		orientSensor = orient;
		this.display = display;

		x1 = new double[2];
		y1 = new double[2]; // for filter
	}

	@Override
	public void onAccuracyChanged(Sensor sensor, int accuracy) {
	}

	@Override
	public void onSensorChanged(SensorEvent event) {
		synchronized (this) {
			orientation = display.getRotation();
			if (event.sensor == accSensor) {
				SensorManager.getRotationMatrix(Rotation, null, event.values, geomagnetic);
				SensorManager.getOrientation(Rotation, angleInRadian);
				if (orientation % 2 == 1) {
					XAngle = (float) sensorsFilter(0, -angleInRadian[2] * 180 / 3.1415926 - 90);
				}
				ZAngle = 0;// angleInRadian[1]*180f/3.1415926f; for rotation
			} else if (event.sensor == orientSensor) {
				if (orientation % 2 == 0) {
					XAngle = (float) sensorsFilter(0, -event.values[1] - 90);
				}
				YAngle = -event.values[0];
				if (YAngle >= 5 && YAngle <= 355) {
					YAngle = -(float) sensorsFilter(1, -YAngle);
				}
				if (orientation % 2 == 0) {
					YAngle += 90;
				}
			}

			//Log.d(TAG, "Sensor updated: " + XAngle + ", " + YAngle + ", " + ZAngle + "; Orientation = " + orientation);
		}
	}
	
	public void onPause() {
		if (logWriter != null) {
			try {
				logWriter.close();
			} catch (IOException e) {
			}
		}
	}
	
	public void onResume() {
		if (logWriter != null) {
			try {
				logWriter.close();
			} catch (IOException e) {
			}
		}
		
		
		//logWriter = new BufferedWriter(new FileWriter(new FileStream))
	}

	public float getXAngle() {
		return XAngle;
	}

	public float getYAngle() {
		return YAngle;
	}

	public float getZAngle() {
		return ZAngle;
	}

	public int getOrientation() {
		return orientation;
	}

	private double sensorsFilter(int index, double val) {
		// return val;

		double y;

		y = 0.7757 * y1[index] + 0.1122 * val + 0.1122 * x1[index]; // IIR
																	// Filter
		y1[index] = y;
		x1[index] = val;
		return y;
	}
}
