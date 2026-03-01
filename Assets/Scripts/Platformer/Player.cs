using UnityEngine;
using System.Collections;

namespace Platformer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D col;
        [SerializeField] private float speed;
        [SerializeField] private float slideSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask groundLayer;

        private bool isGrounded;
        private bool isDoubleJump;
        private bool isMovingDown;
        private bool isWallSliding;
        private Vector3 _baseScale;

        private static readonly int XKey = Animator.StringToHash("X");
        private static readonly int IsJumpKey = Animator.StringToHash("IsJump");
        private const string GroundTag = "Ground";
        private const string WallTag = "Wall";

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
        }

        private void Awake()
        {
            _baseScale = transform.localScale;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, radius);
        }

        private void Update()
        {
            Move();
            WallSlide();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                MoveDown();
            }

            if (isMovingDown)
            {
                if (!IsGrounded())
                {
                    col.enabled = true;
                    isMovingDown = false;
                }
            }
        }

        private void Move()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            animator.SetInteger(XKey, (int)horizontal);
            rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
            Flip(horizontal);
        }

        private void WallSlide()
        {
            if (!isWallSliding)
            {
                return;
            }

            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        }

        private void MoveDown()
        {
            if (!isGrounded)
            {
                return;
            }

            col.enabled = false;
            StartCoroutine(WaitForSeconds(0.5f));
        }

        private void Flip(float horizontal)
        {
            if (horizontal < 0)
            {
                transform.localScale = new Vector3(-_baseScale.x, _baseScale.y, _baseScale.z);
            }
            else if (horizontal > 0)
            {
                transform.localScale = _baseScale;
            }
        }

        private void Jump()
        {
            if (!isGrounded)
            {
                if (!isDoubleJump)
                {
                    DoJump();
                    isDoubleJump = true;
                }
            }
            else
            {
                DoJump();
            }

            return;
            void DoJump()
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            isMovingDown = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision == null)
            { 
                return; 
            }

            if (collision.gameObject.CompareTag(GroundTag))
            {
                isGrounded = true;
                isWallSliding = false;
                isDoubleJump = false;
                animator.SetBool(IsJumpKey, false);
                return;
            }
            
            if (collision.gameObject.CompareTag(WallTag))
            {
                isWallSliding = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision == null)
            {
                return;
            }

            if (collision.gameObject.CompareTag(GroundTag))
            {
                isGrounded = false;
                animator.SetBool(IsJumpKey, true);
            }
            else if (collision.gameObject.CompareTag(WallTag))
            {
                isWallSliding = false;
            }
        }
    }
}
