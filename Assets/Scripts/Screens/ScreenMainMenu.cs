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
		Requests.Instance.sosRequest.Send();
	}

	private void OnClickButtonStartWork()
	{
		StartCoroutine(SendRequestStartWork());
	}

	private IEnumerator SendRequestStartWork()
	{
		yield return null;

		GeneralJava.work.Start();
		Prefs.IsUserWork = true;
		Prefs.DateStartWork = DateTime.Now;

		SetButtonWork(true);
	}

	private void OnClickButtonStopWork()
	{
		StartCoroutine(SendRequestStopWork());
	}

	private void SetButtonWork(bool isStart)
	{
		buttonStartWork.gameObject.SetActive(!isStart);
		buttonStopWork.gameObject.SetActive(isStart);
	}

	private IEnumerator SendRequestStopWork()
	{
		yield return null;

		GeneralJava.work.Stop();
		Prefs.IsUserWork = false;
		Prefs.DateStopWork = DateTime.Now;
		Prefs.LastTimeWork = (float)(Prefs.DateStopWork - Prefs.DateStartWork).TotalHours;

		SetButtonWork(false);
	}

	private void Hide()
	{
		this.gameObject.SetActive(false);
	}

	private void ShowNextScreen(GameObject nextScreen)
	{
		nextScreen.SetActive(true);
	}
}
