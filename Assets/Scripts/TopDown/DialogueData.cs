using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/Data")]
    public class DialogueData : ScriptableObject
    {
        [field: SerializeField] public List<DialogueSentence> sentenceList = new();
    }
}
