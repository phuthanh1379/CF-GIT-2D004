using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

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
