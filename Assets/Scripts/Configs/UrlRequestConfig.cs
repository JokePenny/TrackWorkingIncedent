using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UrlRequestConfig", menuName = "UrlRequestConfig")]
public class UrlRequestConfig : ScriptableObject
{
	[SerializeField] private string urlSOS;
	[SerializeField] private string url1;
	[SerializeField] private string url2;
	[SerializeField] private string url3;

	public string GetUrlSOS()
	{
		return urlSOS;
	}
}
