using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;

public class ServerNotifier : MonoBehaviour {
	
	public int id;
	
	private System.Timers.Timer timer;
	
	// Use this for initialization
	void Start () {
		timer = new System.Timers.Timer(5000);
		timer.Elapsed += new System.Timers.ElapsedEventHandler(worker);
		//timer.Start();
		
		System.Threading.Thread t = new System.Threading.Thread(() => {
			worker(this, null);
			timer.Start();
		});
		t.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy() {
		if (timer != null) 
		{
			timer.Stop();
		}
	}
	
	void OnApplicationPause(bool pause) 
	{
		if (timer != null)
		{
		
			if(pause)
			{
				timer.Stop();
			}
			else
			{
				timer.Start();
			}
		}
	}
	
	void OnApplicationQuit()
	{
		if(timer != null)
		{
			timer.Stop();
		}
	}
	
	void worker(object sender, System.Timers.ElapsedEventArgs e)
	{		
		try 
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.0.10:10080/tsingdroid/loc.ashx?id=" + id.ToString());
			request.Timeout = 2000;
			
			WebResponse response = request.GetResponse();
			Stream data = response.GetResponseStream();
			
	        StreamReader reader = new StreamReader (data);
	        string s = reader.ReadToEnd();
			reader.Close();
			
			Debug.Log("Server Notifier (" + id.ToString() + "): " + s);
		} 
		catch(WebException exception)
		{
			Debug.Log("Server Notifier Exception: " + exception.GetType().Name + ": " + exception.Message);
		}
	}

}
