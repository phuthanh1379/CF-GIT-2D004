using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
    }

    private void OnInputFieldValueChanged(string value)
    {
        GameData.Instance.SetPlayerName(value);
        UnityEngine.Debug.Log(value);
    }

    public void OnClickPlayButton()
    {
        PlayOnClickButtonSound();
        SceneManager.LoadScene("Main");
    }

    public void OnClickQuitButton()
    {
        PlayOnClickButtonSound();
        Application.Quit();
    }

    public void OnClickSettingsButton()
    {
        PlayOnClickButtonSound();
    }

    private void PlayOnClickButtonSound()
    {
        AudioController.Instance.PlayUISoundEffect();
    }
}
