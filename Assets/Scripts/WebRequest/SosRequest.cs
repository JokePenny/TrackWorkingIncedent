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

	public void Send()
	{
		StartCoroutine(SendRequest());
	}

	private IEnumerator SendRequest()
	{
		//SendSOSRequest[] sos = new SendSOSRequest[] { new SendSOSRequest(TypeSendSOS.HAND, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")) };
		//string jsonString = JsonHelper.ToJson<SendSOSRequest>(sos);
		SendSOSRequest sos = new SendSOSRequest(TypeSendSOS.HAND, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
		string jsonString = JsonUtility.ToJson(sos);

		string url = Requests.Instance.urlRequestConfig.GetUrlSOS();

		UnityWebRequest uwr = new UnityWebRequest(url, "POST");
		byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonString);
		uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
		uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("api-key", Prefs.ApiKey);

		Debug.Log(jsonString);
		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError)
		{
			//consoleOutput.text = uwr.error;
			Debug.LogError(uwr.error);
		}
		else
		{
			//consoleOutput.text = uwr.downloadHandler.text;
			Debug.Log(uwr.downloadHandler.text);
		}
	}

	[Serializable]
	private struct SendSOSRequest
	{
		public TypeSendSOS type;
		public string date;
		public SendSOSRequest(TypeSendSOS type, string date)
		{
			this.type = type;
			this.date = date;
		}
	}
}
