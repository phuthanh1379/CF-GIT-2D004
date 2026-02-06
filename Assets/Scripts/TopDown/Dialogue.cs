using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TopDown
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private TMP_Text contentText;
        [SerializeField] private CanvasGroup canvasGroup;

        public void Show(string content)
        {
            canvasGroup.alpha = 1;
            contentText.text = content;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
        }
    }
}
