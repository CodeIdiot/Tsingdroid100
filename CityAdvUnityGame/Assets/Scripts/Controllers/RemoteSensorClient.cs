using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class RemoteSensorClient : MonoBehaviour {
	
	private readonly string HOST = "127.0.0.1";
	private const int PORT = 65001;
	private const float RETRY_TIME = 1.0f;	
	
	private static RemoteSensorClient instance;
	public static RemoteSensorClient Instance {
		get {return instance;}
	}
	
	private TcpClient tcpClient;
	private Thread workingThread;
	
	private float lastTrialTime = 0;
	private bool locationEnabled = false, orientationEnabled = false;
	private Vector3 cameraAngles;
	private double latitude, longitude;
	private bool closeRequired = false;
	
	public bool LocationEnabled { get {return locationEnabled; } }
	public bool OrientationEnabled { get {return orientationEnabled; }}
	public Vector3 CameraAngles { get {return cameraAngles; }}
	public double Latitude {get {return latitude; }}
	public double Longitude {get {return longitude; }}
	
	// Use this for initialization
	void Start () {
		if (Application.platform == RuntimePlatform.Android) {
			instance = null;
		} else {
			instance = this;
		}
	}
	
	void Update() {
		if (Application.platform == RuntimePlatform.Android) {
			return;
		}
		
		if (Time.time - lastTrialTime > RETRY_TIME
		    && (workingThread == null || !workingThread.IsAlive)
		    && (tcpClient == null || !tcpClient.Connected)) {
			connect();
		}
	}
	
	void OnDestroy() {
		if (Application.platform == RuntimePlatform.Android) {
			return; 
		}
		
		close();
	}
	
	private void connect() {
		workingThread = new Thread(threadStart);
		workingThread.Start();
	}
	
	private void close() {
		closeRequired = true;
	}
	
	private void threadStart() {
		try {
			tcpClient = new TcpClient(HOST, PORT);
			if(tcpClient == null || !tcpClient.Connected) {
				return;
			}
			Debug.Log("Connected to sensor server " + HOST + " : " + PORT);
			using (StreamReader reader = new StreamReader(tcpClient.GetStream(), Encoding.UTF8)) {
				string line;
				while ((line = reader.ReadLine()) != null) {
					//Debug.Log("Receive: " + line);
					if (line.StartsWith("OE")) {
						orientationEnabled = true;
						string[] elements = line.Substring(3).Split(new char[] {' '});
						cameraAngles = new Vector3(float.Parse(elements[0]), 
						                           float.Parse(elements[1]),
						                           float.Parse(elements[2]));
					} else if (line.StartsWith("OD")) {
						orientationEnabled = false;
					} else if (line.StartsWith("LE")) {
						locationEnabled = true;
						string[] elements = line.Substring(3).Split(new char[] {' '});
						latitude = double.Parse(elements[0]);
						longitude = double.Parse(elements[1]);
					} else if (line.StartsWith("LD")) {
						locationEnabled = false;
					}
					
					if (closeRequired) {
						orientationEnabled = false;
						locationEnabled = false;
						tcpClient.Close();
						break;
					}
				}
			}
		} catch (SocketException e) {
			orientationEnabled = false;
			locationEnabled = false;
			Debug.LogError("Exception: " + e.Message);
		}
	}
}
