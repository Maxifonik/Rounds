using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI _errorText;

    [SerializeField] private Button _okButton;

    public void SetErrorText(string value)
    {
        _errorText.text = value;
    }
    private void Start()
    {
        _okButton.onClick.AddListener(OkButtonClick);
    }
    private void OkButtonClick()
    {
        ScreensController.Current.ShowPrevScreen();
    }
}