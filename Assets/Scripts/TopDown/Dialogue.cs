using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace TopDown
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private TMP_Text contentText;
        [SerializeField] private CanvasGroup canvasGroup;

        public event Action CompleteShowDialogue;

        public void Show(string content)
        {
            canvasGroup.alpha = 1;
            //contentText.text = content;

            var showDialogueTween = DOTween.To(
                () => string.Empty, // Gia tri khoi dau
                x => contentText.text = x, // Gia tri trong thoi gian Tween
                content, // Gia tri cuoi cung
                5f); // Thoi gian thuc hien Tween

            showDialogueTween
                .Play()
                .OnComplete(OnComplete)
                ;

            void OnComplete()
            {
                CompleteShowDialogue?.Invoke();
            }
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
        }
    }
}
