using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Spaceship player;
    [SerializeField] private GameObject pauseMenuGameObject;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text bulletCountText;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TMP_Text playerNameText;

    private void Start()
    {
        pauseMenuGameObject.SetActive(false);
        playerNameText.text = MenuController.Instance._inputFieldValue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuGameObject.SetActive(true);
        }

        healthText.text = $"Health: {player.CurrentHealth}";
        healthBarImage.fillAmount = (float)player.CurrentHealth / player.HealthMax;
        bulletCountText.text = $"Bullet: {player.BulletCount}/{player.BulletMax}";
    }

    public void OnClickQuitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
