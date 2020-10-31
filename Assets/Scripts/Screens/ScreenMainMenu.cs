using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMainMenu : MonoBehaviour
{
	[Header("Экраны")]
	[SerializeField] private ScreenEnterApp screenEnterApp;

	[Header("Алерты")]
	[SerializeField] private TextMeshProUGUI alertAttention;

	[Header("Поля для заполнния")]
	[SerializeField] private TMP_Dropdown dropdownFirst;
	[SerializeField] private TMP_Dropdown dropdownSecond;

	[Header("Кнопки")]
	[SerializeField] private Button buttonBack;
	[SerializeField] private Button buttonSos;
	[SerializeField] private Button buttonStartWork;
	[SerializeField] private Button buttonStopWork;

	[Header("Инфо блок")]
	[SerializeField] private TextMeshProUGUI textNameUser;
	[SerializeField] private TextMeshProUGUI textCareerUser;


	private void Awake()
	{
		buttonBack.onClick.AddListener(OnClickButtonBack);
		buttonSos.onClick.AddListener(OnClickButtonSos);
		buttonStartWork.onClick.AddListener(OnClickButtonStartWork);
		buttonStopWork.onClick.AddListener(OnClickButtonStopWork);
	}

	private void OnEnable()
	{
		alertAttention.gameObject.SetActive(false);
	}

	private void OnClickButtonBack()
	{
		ShowNextScreen(screenEnterApp.gameObject);
		Hide();
	}

	private void OnClickButtonSos()
	{
		StartCoroutine(SendRequestSOS());
	}

	private IEnumerator SendRequestSOS()
	{
		SosRequest sos = Requests.Instance.sosRequest;
		yield return StartCoroutine(sos.SendRequest());

		switch (sos.ResponseCode)
		{
			case Requests.RESPONSE_CODE_SUCCESS:
				ShowAttention("! сигнал отправлен диспетчеру !");
				break;
			case Requests.RESPONSE_CODE_BAD_GATEAWAY:
				ShowAttention("! сервер неактивен !");
				break;
		}
	}

	private void OnClickButtonStartWork()
	{
		StartCoroutine(SendRequestStartWork());
	}

	private IEnumerator SendRequestStartWork()
	{
		WorkRequest work = Requests.Instance.workRequest;
		yield return StartCoroutine(work.SendRequest(WorkRequest.StatusWork.START_WORK));

		switch (work.ResponseCode)
		{
			case Requests.RESPONSE_CODE_SUCCESS:
				GeneralJava.work.Start();
				Prefs.IsUserWork = true;
				Prefs.DateStartWork = DateTime.Now;
				SetButtonWork(true);
				ShowAttention("! смена началась !");
				break;
			case Requests.RESPONSE_CODE_BAD_REQUEST:
				ShowAttention("! вы уже работаете !");
				break;
			case Requests.RESPONSE_CODE_BAD_GATEAWAY:
				ShowAttention("! сервер неактивен !");
				break;
		}
	}

	private void OnClickButtonStopWork()
	{
		StartCoroutine(SendRequestStopWork());
	}

	private IEnumerator SendRequestStopWork()
	{
		WorkRequest work = Requests.Instance.workRequest;
		yield return StartCoroutine(work.SendRequest(WorkRequest.StatusWork.STOP_WORK));

		switch (work.ResponseCode)
		{
			case Requests.RESPONSE_CODE_SUCCESS:
				GeneralJava.work.Stop();
				Prefs.IsUserWork = false;
				Prefs.DateStopWork = DateTime.Now;
				Prefs.LastTimeWork = (float)(Prefs.DateStopWork - Prefs.DateStartWork).TotalHours;
				SetButtonWork(false);
				ShowAttention("! смена началась !");
				break;
			case Requests.RESPONSE_CODE_BAD_REQUEST:
				ShowAttention("! вы уже работаете !");
				break;
			case Requests.RESPONSE_CODE_BAD_GATEAWAY:
				ShowAttention("! сервер неактивен !");
				break;
		}
	}

	private void SetButtonWork(bool isStart)
	{
		buttonStartWork.gameObject.SetActive(!isStart);
		buttonStopWork.gameObject.SetActive(isStart);
	}

	private void Hide()
	{
		this.gameObject.SetActive(false);
	}

	private void ShowNextScreen(GameObject nextScreen)
	{
		nextScreen.SetActive(true);
	}

	private void ShowAttention(string attentionText)
	{
		alertAttention.text = attentionText;
		alertAttention.transform.parent.gameObject.SetActive(true);
	}

	private void HideAttention()
	{
		alertAttention.transform.parent.gameObject.SetActive(false);
	}
}
