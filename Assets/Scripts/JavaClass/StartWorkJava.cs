using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWorkJava
{
	public void Start()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			string apiKey = Prefs.ApiKey;
			if (!string.IsNullOrEmpty(apiKey)) GeneralJava.PluginInstance.Call("setApiKey", apiKey);

			if (GeneralJava.PluginInstance.Call<int>("startAccelerometerService") == 1) Debug.Log("Start AccelerometerService");
			else Debug.LogError("Not Start AccelerometerService");

			if (GeneralJava.PluginInstance.Call<int>("startGPSService") == 1) Debug.Log("Start GPSService");
			else Debug.LogError("Not Start GPSService");
		}
	}

	public void Stop()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			string apiKey = Prefs.ApiKey;
			if (!string.IsNullOrEmpty(apiKey)) GeneralJava.PluginInstance.Call("setApiKey", apiKey);

			if (GeneralJava.PluginInstance.Call<int>("stopAccelerometerService") == 1) Debug.Log("Stop AccelerometerService");
			else Debug.LogError("Not Stop stopAccelerometerService");

			if (GeneralJava.PluginInstance.Call<int>("stopGPSService") == 1) Debug.Log("Stop GPSService");
			else Debug.LogError("Not Stop GPSService");
		}
	}
}
