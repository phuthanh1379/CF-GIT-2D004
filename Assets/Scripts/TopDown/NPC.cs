using UnityEngine;

namespace TopDown
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private DialogueData dialogueData;
        public DialogueData DialogueData => dialogueData;
    }
}
