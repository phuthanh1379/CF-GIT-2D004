using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private PlayerProfile playerProfile;

    public static GameData Instance;

    public string PlayerName => playerProfile == null ? string.Empty : playerProfile.PlayerName;
    public PlayerProfile PlayerProfile => playerProfile;

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName(string playerName)
    {
        playerProfile?.SetPlayerName(playerName);
    }
}
