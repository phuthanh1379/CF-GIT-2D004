using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text welcomeText;

    [Header("SFX Volume")]
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private TMP_Text sfxVolumeValueText;

    [Header("BGM Volume")]
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private TMP_Text bgmVolumeValueText;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderValueChanged);
        bgmVolumeSlider.onValueChanged.AddListener(OnBGMVolumeSliderValueChanged);
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
        sfxVolumeSlider.onValueChanged.RemoveListener(OnSFXVolumeSliderValueChanged);
        bgmVolumeSlider.onValueChanged.RemoveListener(OnBGMVolumeSliderValueChanged);
    }

    private void OnInputFieldValueChanged(string value)
    {
        GameData.Instance.SetPlayerName(value);
    }

    private void OnSFXVolumeSliderValueChanged(float value)
    {
        AudioController.Instance.SetVolumeSFX(sfxVolumeSlider.value);
        AudioController.Instance.PlayButtonClickSFX();
        sfxVolumeValueText.text = $"SFX: {RoundVolumeValue(value)}";
    }

    private void OnBGMVolumeSliderValueChanged(float value)
    {
        AudioController.Instance.SetVolumeBGM(bgmVolumeSlider.value);
        bgmVolumeValueText.text = $"BGM: {RoundVolumeValue(value)}";
    }

    private void Start()
    {
        var playerName = GameData.Instance.PlayerName;
        var playerNameValid = string.IsNullOrEmpty(playerName);
        inputField.gameObject.SetActive(playerNameValid);
        welcomeText.gameObject.SetActive(!playerNameValid);
        welcomeText.text = $"Welcome back, {playerName}";

        bgmVolumeSlider.value = AudioController.Instance.VolumeBGM;
        bgmVolumeValueText.text = $"BGM: {RoundVolumeValue(bgmVolumeSlider.value)}";

        sfxVolumeSlider.value = AudioController.Instance.VolumeSFX;
        sfxVolumeValueText.text = $"SFX: {RoundVolumeValue(sfxVolumeSlider.value)}";
    }

    private int RoundVolumeValue(float value) => (int)(value * 100);

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
        AudioController.Instance.PlayButtonClickSFX();
    }
}
