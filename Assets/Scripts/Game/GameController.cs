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

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    private void Awake()
    {
        player.PlayerDead += OnPlayerDead;
    }

    private void OnDestroy()
    {
        player.PlayerDead -= OnPlayerDead;
    }

    private void Start()
    {
        pauseMenuGameObject.SetActive(false);
        playerNameText.text = GameData.Instance.PlayerName;
        gameOverUI.SetActive(false);
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

    private void OnPlayerDead()
    {
        gameOverUI.SetActive(true);
    }
}
