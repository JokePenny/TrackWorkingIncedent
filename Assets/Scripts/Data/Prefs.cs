using System;
using UnityEngine;

public class Prefs
{
	public static string ApiKey
	{
		get{return PlayerPrefs.GetString("api_key_user", ""); }
		set { PlayerPrefs.SetString("api_key_user", value); }
	}

	public static DateTime SetDateStartWork
	{
		get { return DateTime.Parse(PlayerPrefs.GetString("date_start_work", DateTime.Now.ToString())); }
		set { PlayerPrefs.SetString("date_start_work", value.ToString()); }
	}
}
