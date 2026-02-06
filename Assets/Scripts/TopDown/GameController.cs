using UnityEngine;

namespace TopDown
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private Player player;
        [SerializeField] private GameObject closeDialogueGameObject;

        private bool _canCloseDialogue;

        private void Awake()
        {
            if (player != null)
            {
                player.TalkToNPC += OnTalkToNPC;
            }

            if (dialogue != null)
            {
                dialogue.CompleteShowDialogue += OnCompleteShowDialogue;
            }
        }

        private void OnDestroy()
        {
            if (player != null)
            {
                player.TalkToNPC -= OnTalkToNPC;
            }


            if (dialogue != null)
            {
                dialogue.CompleteShowDialogue -= OnCompleteShowDialogue;
            }
        }

        private void Start()
        {
            dialogue.Init(player.Avatar);
            dialogue.Hide();
            closeDialogueGameObject.SetActive(false);
        }

        private void Update()
        {
            CheckToCloseDialogue();
        }

        private void CheckToCloseDialogue()
        {
            if (!_canCloseDialogue)
            {
                return;
            }

            if (Input.anyKeyDown)
            {
                dialogue.Hide();
                closeDialogueGameObject.SetActive(false);
                player.SetMovable(true);
            }
        }

        private void OnTalkToNPC(DialogueData data)
        {
            _canCloseDialogue = false;
            dialogue.Show(data);
        }

        private void OnCompleteShowDialogue()
        {
            _canCloseDialogue = true;
            closeDialogueGameObject.SetActive(true);
        }
    }
}