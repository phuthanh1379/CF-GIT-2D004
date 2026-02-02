using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleProfile", menuName = "Data/ObstacleProfile")]
public class ObstacleProfile : ScriptableObject
{
    [field: SerializeField] public int Score { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}