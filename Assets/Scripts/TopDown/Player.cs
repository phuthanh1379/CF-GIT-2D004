using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float speed;

        private void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            transform.Translate(speed * Time.deltaTime * new Vector2(horizontal, vertical));
            animator.SetFloat("X", horizontal);
            animator.SetFloat("Y", vertical);
        }
    }
}
