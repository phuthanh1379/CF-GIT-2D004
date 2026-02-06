using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TopDown
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private Player player;

        private void Awake()
        {
            if (player == null)
            {
                return;
            }

            player.TalkToNPC += OnTalkToNPC;
        }

        private void OnDestroy()
        {
            if (player == null)
            {
                return;
            }

            player.TalkToNPC -= OnTalkToNPC;
        }

        private void Start()
        {
            dialogue.Hide();
        }

        private void OnTalkToNPC(string dialogueContent)
        {
            dialogue.Show(dialogueContent);
        }
    }
}