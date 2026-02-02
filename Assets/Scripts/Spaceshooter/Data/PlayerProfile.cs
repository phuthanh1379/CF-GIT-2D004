using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "Data/PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    [field: SerializeField] public string PlayerName { get; private set; }
    [field: SerializeField] public int PlayerHealth { get; private set; }
    [field: SerializeField] public int PlayerHealthMax { get; private set; }
    [field: SerializeField] public int PlayerScore { get; private set; }
    
    public void SetPlayerName(string value) => this.PlayerName = value;
    public void SetPlayerHealth (int value) => this.PlayerHealth = value;
    public void SetPlayerHealthMax(int value) => this.PlayerHealthMax = value;
    public void SetPlayerScore(int value) => this.PlayerScore = value;
    public void AddPlayerScore(int value) => this.PlayerScore += value;
}
