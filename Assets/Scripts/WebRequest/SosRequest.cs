using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SosRequest : MonoBehaviour
{
	private enum TypeSendSOS
	{
		HAND = 1,
		AUTHO = 2
	}

	public int ResponseCode { get; private set; }

	public IEnumerator SendRequest(int idConstruction)
	{
		SOSRequestSend sos = new SOSRequestSend(TypeSendSOS.HAND, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), idConstruction);
		string jsonString = "[" + JsonUtility.ToJson(sos) + "]";

		string url = Requests.Instance.urlRequestConfig.UrlSOS;

		UnityWebRequest uwr = new UnityWebRequest(url, "POST");
		byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonString);
		uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
		uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("api-key", Prefs.ApiKey);

		Debug.Log(jsonString);
		yield return uwr.SendWebRequest();

		ResponseCode = (int)uwr.responseCode;
		if (uwr.isNetworkError)
		{
			Debug.LogError(uwr.error);
		}
		else
		{
			Debug.Log(uwr.downloadHandler.text);
		}
	}

	[Serializable]
	private struct SOSRequestSend
	{
		public TypeSendSOS type;
		public string date;
		public int constructionId;
		public SOSRequestSend(TypeSendSOS type, string date, int constructionId)
		{
			this.type = type;
			this.date = date;
			this.constructionId = constructionId;
		}
	}
}
