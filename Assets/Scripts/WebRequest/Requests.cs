using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Requests : MonoBehaviour
{
	public static Requests Instance { get; private set; }
	public UrlRequestConfig urlRequestConfig { get; private set; }
	public SosRequest sosRequest { get; private set; }

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}

		urlRequestConfig = Resources.Load<UrlRequestConfig>("UrlRequestConfig");
		sosRequest = GetComponent<SosRequest>();
	}
}
