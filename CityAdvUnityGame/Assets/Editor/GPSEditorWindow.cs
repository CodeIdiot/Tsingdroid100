using UnityEngine;
using System.Collections;
using UnityEditor;

public class GPSEditorWindow : EditorWindow {
	
	private double latitude, longitude;
	private int latitudeDegree, latitudeMinute, longitudeDegree, longitudeMinute;
	private float latitudeSecond, longitudeSecond;
	
	private Vector2 prevPosition = Vector2.zero;
	private bool moveFoldOut = false;
	
	[MenuItem("Window/GPS Editor")]
	static void Init() {
		EditorWindow.GetWindow<GPSEditorWindow>(false, "GPS Editor");
	}
	
	void Update() {
		Transform t = Selection.activeTransform;
		Vector2 position;
		if(t == null) {
			position = Vector2.zero;
		} else {
			position = new Vector2(t.position.x, t.position.z);
		}
		
		if(position == prevPosition) {
			return;
		}
		
		GPSBeaconManager manager = new GPSBeaconManager();
		manager.GetGpsPosition(t.position, out latitude, out longitude);
		toParts(latitude, out latitudeDegree, out latitudeMinute, out latitudeSecond);
		toParts(longitude, out longitudeDegree, out longitudeMinute, out longitudeSecond);
		Repaint();
		prevPosition = position;
	}
	
	void OnGUI ()
	{	
		EditorGUILayout.LabelField("Latitude", latitude.ToString());
		EditorGUILayout.LabelField("Longitude", longitude.ToString());
		
		moveFoldOut = EditorGUILayout.Foldout(moveFoldOut, "Move to GPS Position");
		if(moveFoldOut) {
			latitudeDegree = EditorGUILayout.IntField("Latitude Degree", latitudeDegree);
			latitudeMinute = EditorGUILayout.IntField("Latitude Minute", latitudeMinute);
			latitudeSecond = EditorGUILayout.FloatField("Latitude Second", latitudeSecond);
			longitudeDegree = EditorGUILayout.IntField("Longitude Degree", longitudeDegree);
			longitudeMinute = EditorGUILayout.IntField("Longitude Minute", longitudeMinute);
			longitudeSecond = EditorGUILayout.FloatField("Longitude Second", longitudeSecond);
		}
		
		if(GUI.changed) {
			double newLatitude = fromParts(latitudeDegree, latitudeMinute, latitudeSecond);
			double newLongitude = fromParts(longitudeDegree, longitudeMinute, longitudeSecond);
			Transform t = Selection.activeTransform;
			GPSBeaconManager manager = new GPSBeaconManager();
			Undo.RegisterUndo(t, "Change GPS Position");
			t.position = manager.GetPosition(newLatitude, newLongitude);
			
		}
	}
	
	private double fromParts(int degree, int minute, double second) {
		return (double)degree + minute / 60.0 + second / 3600.0;
	}
	
	private void toParts(double union, out int degree, out int minute, out float second) {
		degree = (int)union;
		double minuteDbl = (union - degree) * 60;
		minute = (int)minuteDbl;
		second = (float)((minuteDbl - minute) * 60);
	}
}
