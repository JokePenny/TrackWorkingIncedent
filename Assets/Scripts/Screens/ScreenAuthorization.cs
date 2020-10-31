using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAuthorization : MonoBehaviour
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
	[SerializeField] private Button buttonEnter;


	private void Awake()
	{
		buttonBack.onClick.AddListener(OnClickButtonBack);
		buttonEnter.onClick.AddListener(OnClickButtonEnter);
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

	private void OnClickButtonEnter()
	{
		if(string.IsNullOrEmpty(inputFieldEmail.text) || string.IsNullOrEmpty(inputFieldPassword.text))
		{
			ShowAttention("! Заполнены не все поля !");
			return;
		}

		StartCoroutine(SendRequestLogIn());
	}

	private IEnumerator SendRequestLogIn()
	{
		AuthorizationRequest authorization = Requests.Instance.authorizationRequest;
		yield return StartCoroutine(authorization.SendRequest(inputFieldEmail.text, inputFieldPassword.text));

		switch (authorization.ResponseCode)
		{
			case Requests.RESPONSE_CODE_SUCCESS:
				GeneralJava.permission.GetPermissionGeometria();
				ShowNextScreen(screenMainMenu.gameObject);
				Hide();
				break;
			case Requests.RESPONSE_CODE_FORBIDEN:
				ShowAttention("! неверный пароль !");
				break;
			case Requests.RESPONSE_CODE_BAD_REQUEST:
				ShowAttention("! пользователя с таким логином нет !");
				break;
			case Requests.RESPONSE_CODE_BAD_GATEAWAY:
				ShowAttention("! сервер неактивен !");
				break;
		}
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
		alertAttention.gameObject.SetActive(true);
	}
}
