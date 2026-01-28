using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private TMP_InputField inputField;

    public static MenuController Instance;

    public string _inputFieldValue;

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        _inputFieldValue = inputField.text;
        UnityEngine.Debug.Log(_inputFieldValue);
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
        audioSource.PlayOneShot(clip);
    }
}
