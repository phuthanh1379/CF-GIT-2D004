using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private string dialogue;
        public string DialogueContent => dialogue;
    }
}
