using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;

        private Sequence _colorSequence;

        private void Start()
        {
            var moveToEnd = transform.DOMove(endPosition, 2f);
            var moveToStart = transform.DOMove(startPosition, 2f);
            var endColorTween = spriteRenderer.DOColor(endColor, 0.5f);
            var startColorTween = spriteRenderer.DOColor(startColor, 0.5f);

            //Sequence sequence = DOTween.Sequence();
            //sequence
            //    .Append(moveToEnd)
            //    .Join(endColorTween)
            //    .Append(moveToStart)
            //    .Join(startColorTween)
            //    .Play()
            //    .SetLoops(-1);

            _colorSequence = DOTween.Sequence();
            _colorSequence
                .Append(endColorTween)
                .Append(startColorTween)
                .SetAutoKill(false)
                .Pause()
                ;
        }

        public void OnHit()
        {
            //StartCoroutine(OnHit(0.5f));
            _colorSequence.Restart();
        }

        private IEnumerator OnHit(float time)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(time);
            spriteRenderer.color = Color.white;
        }
    }
}