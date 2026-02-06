using System;
using UnityEngine;

namespace TopDown
{
    [Serializable]
    public class DialogueSentence
    {
        [field: SerializeField] public DialogueCharacter Character { get; private set; }
        [field: SerializeField] public string Sentence { get; private set; }
    }
}
