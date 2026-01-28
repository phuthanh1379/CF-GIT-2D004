using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip uiSFX;
    [SerializeField] private List<AudioClip> shootSFXList = new();

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayUISoundEffect()
    {
        PlaySFX(uiSFX);
    }

    public void PlayShootSFX()
    {
        if (audioSource == null || shootSFXList == null || shootSFXList.Count <= 0)
        {
            return;
        }

        var rnd = new System.Random();
        var randomIndex = rnd.Next(0, shootSFXList.Count);
        var clip = shootSFXList[randomIndex];

        PlaySFX(clip);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (audioSource == null || clip == null)
        {
            return;
        }

        audioSource.PlayOneShot(clip);
    }
}
