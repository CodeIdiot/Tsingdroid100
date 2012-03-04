using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData : MonoBehaviour {
	
	private static string DATA_FILE = "gamedata.bin";
	FileStream fileStream;
	BinaryFormatter binaryFormatter;
	
	private static GameData instance;
	public static GameData Instnace {
		get {return instance; }
	}
	
	private Dictionary<string, object> data;
	
	// Use this for initialization
	void Start () {
		instance = this;
		fileStream = new FileStream(Application.persistentDataPath + "/" + DATA_FILE,
		                            FileMode.OpenOrCreate);
		binaryFormatter = new BinaryFormatter();
		load();
		
		Debug.Log("GameData initialized at " + Application.persistentDataPath + "/" + DATA_FILE);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy() {
		if(fileStream != null) {
			fileStream.Close();
			Debug.Log("GameData close.");
		}
	}
	
	private void save() {
		fileStream.Seek(0, SeekOrigin.Begin);
		fileStream.SetLength(0);
		binaryFormatter.Serialize(fileStream, data);
		fileStream.Flush();
		//TODO: IOException?
	}
	
	private void load() {
		fileStream.Seek(0, SeekOrigin.Begin);
		if(fileStream.Length == 0) {
			data = new Dictionary<string, object>(); 
		} else {
			data = binaryFormatter.Deserialize(fileStream) as Dictionary<string, object>;
		}
	}
	
	/// <summary>
	/// Get or set the value of key. When getting value, if the key does not exist, null is returned.
	/// </summary>
	/// <param name="key">
	/// A <see cref="System.String"/>
	/// </param>
	public object this[string key] {
		get {
			if (data.ContainsKey(key)) {
				return data[key];
			} else {
				return null;
			}
		}
		
		set {
			data[key] = value;
			save();
		}
	}
	
	/// <summary>
	/// Get the value of key, assuming the value is integer. If the key does not exist, 0 is returned. If the value is not integer, an integer represting this value is returned. (float: (int)value; bool: value ? 1 : 0; string: value.length; other: 0)
	/// </summary>
	/// <param name="key">
	/// A <see cref="System.String"/>
	/// </param>
	public static int GetInt(string key) {
		if (instance.data.ContainsKey(key)) {
			object obj = instance.data[key];
			if (obj is int) {
				return (int)obj;
			} else if (obj is float) {
				return (int)(float)obj;
			} else if (obj is string) {
				return (obj as string).Length;
			} else if (obj is bool) {
				return ((bool)obj) ? 1 : 0;
			} else {
				return 0;
			}
		} else {
			return 0;
		}
	}
	
	/// <summary>
	/// Set a data with key and value
	/// </summary>
	/// <param name="key">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="value">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void SetInt(string key, int value) {
		instance.data[key] = value;
		instance.save();
	}
	
	/// <summary>
	/// Get the value of key, assuming the value is float. If the key does not exist, 0 is returned. If the value is not float, an float represting this value is returned. (int: (float)value; bool: value ? 1.0f : 0.0f; other: float.NaN)
	/// </summary>
	/// <param name="key">
	/// A <see cref="System.String"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.Single"/>
	/// </returns>
	public static float GetFloat(string key) {
		if (instance.data.ContainsKey(key)) {
			object obj = instance.data[key];
			if (obj is float) {
				return (float)obj;
			} else if (obj is int) {
				return (float)(int)obj;
			} else if (obj is bool) {
				return (bool)obj ? 1.0f : 0.0f;
			} else {
				return float.NaN;
			}
		} else {
			return 0.0f;
		}
	}
	
	public static void SetFloat(string key, float value) {
		instance.data[key] = value;
		instance.save();
	}
	
	/// <summary>
	/// Get the value of key, assuming the value is bool. If the key does not exist, false is returned. If the value is not bool, a bool represting this value is returned. (int/float: value != 0; string: value.length > 0; other: false)
	/// </summary>
	/// <param name="key">
	/// A <see cref="System.String"/>
	/// </param>
	public static bool GetBool(string key) {
		if (instance.data.ContainsKey(key)) {
			object obj = instance.data[key];
			if (obj is bool) {
				return (bool)obj;
			} else if (obj is int) {
				return (int)obj != 0;
			} else if (obj is float) {
				return (float)obj != 0.0f;
			} else if (obj is string) {
				return (obj as string).Length > 0;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
	
	public static void SetBool(string key, bool value) {
		instance.data[key] = value;
		instance.save();
	}
	
	/// <summary>
	/// Get the value of key, assuming the value is string. If the key does not exist, null is returned. If the value is not string, an string represting this value is returned. (object: value.ToString(); null: null)
	/// </summary>
	/// <param name="key">
	/// A <see cref="System.String"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.String"/>
	/// </returns>
	public static string GetString(string key) {
		if (instance.data.ContainsKey(key)) {
			object obj = instance.data[key];
			if (obj != null) {
				return obj.ToString();
			} else {
				return null;
			}
		} else {
			return null;
		}
	}
	
	public static void SetString(string key, string value) {
		instance.data[key] = value;
		instance.save();
	}
	
	public static object GetObject(string key) {
		if(instance.data.ContainsKey(key)) {
			return instance.data[key]; 
		} else {
			return null;
		}
	}
	
	public static void SetObject(string key, object value) {
		instance.data[key] = value;
		instance.save();
	}
}
