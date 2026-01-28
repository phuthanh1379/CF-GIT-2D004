using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public string PlayerName { get; private set; }

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
        PlayerName = playerName;
    }
}
