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
	}

	private void OnClickButtonStartWork()
	{
		StartCoroutine(SendRequestStartWork());
	}

	private IEnumerator SendRequestStartWork()
	{
		yield return null;
		buttonStopWork.gameObject.SetActive(true);
		buttonStartWork.gameObject.SetActive(false);
	}

	private void OnClickButtonStopWork()
	{
		StartCoroutine(SendRequestStopWork());
		buttonStartWork.gameObject.SetActive(true);
		buttonStopWork.gameObject.SetActive(false);
	}

	private IEnumerator SendRequestStopWork()
	{
		yield return null;
	}

	private void OnClickButtonEnter()
	{
		//if (string.IsNullOrEmpty(dropdownFirst.) || string.IsNullOrEmpty(inputFieldPassword.text))
		//{
		//	alertAttention.text = "! Заполнены не все поля !";
		//	alertAttention.gameObject.SetActive(true);
		//	return;
		//}

		StartCoroutine(SendRequestLogIn());
	}

	private IEnumerator SendRequestLogIn()
	{
		yield return null;
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
