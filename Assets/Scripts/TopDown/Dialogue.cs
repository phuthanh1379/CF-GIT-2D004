using System.Collections;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private TMP_Text contentText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image playerImage;
        [SerializeField] private Image npcImage;

        public event Action CompleteShowDialogue;

        private Tween _showDialogueTween;
        private string _content;

        public void Init(Sprite playerAvatar)
        {
            playerImage.sprite = playerAvatar;
        }

        private Tween ShowSentence(DialogueSentence sentence)
        {
            _content = sentence.Sentence;
            npcImage.sprite = sentence.Character.Avatar;

            _showDialogueTween = DOTween.To(
                () => string.Empty, // Gia tri khoi dau
                x => contentText.text = x, // Gia tri trong thoi gian Tween
                _content, // Gia tri cuoi cung
                2f); // Thoi gian thuc hien Tween

            return _showDialogueTween
                .OnStart(OnStart)
                .SetTarget(this)
                ;

            void OnStart()
            {
                if (string.Equals(sentence.Character.Name, "Player"))
                {
                    playerImage.color = Color.white;
                    npcImage.color = Color.red;
                }
                else
                {
                    playerImage.color = Color.red;
                    npcImage.color = Color.white;
                }
            }
        }

        public void Show(DialogueData data)
        {
            canvasGroup.alpha = 1;
            if (data == null || data.sentenceList == null || data.sentenceList.Count <= 0)
            {
                return;
            }

            var sentences = data.sentenceList;
            var sequence = DOTween.Sequence();
            for (var i = 0; i < sentences.Count; i++)
            {
                sequence.Append(ShowSentence(sentences[i]));
            }

            sequence.OnComplete(() => CompleteShowDialogue?.Invoke())
                .Play();
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
        }

        private void Skip()
        {
            if (_showDialogueTween == null || string.Equals(contentText.text, _content))
            {
                return;
            }

            DOTween.Kill(this);
            contentText.text = _content;
            //StartCoroutine(WaitForSeconds());
            CompleteShowDialogue?.Invoke();
        }

        private IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(0.01f);
        }

        private void Update()
        {
            //if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    Skip();
            //}
        }
    }
}
