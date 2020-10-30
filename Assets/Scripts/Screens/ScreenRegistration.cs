using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRegistration : MonoBehaviour
{
	[Header("Экраны")]
	[SerializeField] private ScreenEnterApp screenEnterApp;
	[SerializeField] private ScreenMainMenu screenMainMenu;

	[Header("Алерты")]
	[SerializeField] private TextMeshProUGUI alertAttention;

	[Header("Поля для заполнния")]
	[SerializeField] private TMP_InputField inputFieldEmail;
	[SerializeField] private TMP_InputField inputFieldPassword;

	[Header("Кнопки")]
	[SerializeField] private Button buttonBack;
	[SerializeField] private Button buttonRegistration;

	private void Awake()
	{
		buttonBack.onClick.AddListener(OnClickButtonBack);
		buttonRegistration.onClick.AddListener(OnClickButtonRegistration);
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

	private void OnClickButtonRegistration()
	{
		if (string.IsNullOrEmpty(inputFieldEmail.text) || string.IsNullOrEmpty(inputFieldPassword.text))
		{
			alertAttention.text = "! Заполнены не все поля !";
			alertAttention.gameObject.SetActive(true);
			return;
		}

		StartCoroutine(SendRequestLogIn());
	}

	private IEnumerator SendRequestLogIn()
	{
		yield return null;
		ShowNextScreen(screenMainMenu.gameObject);
		Hide();
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
