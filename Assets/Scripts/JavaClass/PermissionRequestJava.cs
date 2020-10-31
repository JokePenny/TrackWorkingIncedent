using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionRequestJava
{
    public void GetPermissionGeometria()
    {
		if (Application.platform == RuntimePlatform.Android)
		{
			if (GeneralJava.PluginInstance.Call<int>("getPermissionGeometria") == 1) Debug.Log("Success getPermissionGeometria");
			else Debug.LogError("Failed getPermissionGeometria");
		}
	}
}
