using UnityEngine;

namespace TopDown
{
    [CreateAssetMenu(fileName = "Character", menuName = "Dialogue/Character")]
    public class DialogueCharacter : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Avatar { get; private set; }
    }
}
