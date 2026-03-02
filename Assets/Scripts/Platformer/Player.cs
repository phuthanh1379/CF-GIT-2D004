using UnityEngine;
using System.Collections;

namespace Platformer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D col;
        [SerializeField] private ParticleSystem bloodVFX;
        [SerializeField] private ParticleSystem dustVFX;

        [Header("Values")]
        [SerializeField] private float speed;
        [SerializeField] private float slideSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float stompForce;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask groundLayer;

        [Header("Test")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool canDoubleJump;
        [SerializeField] private bool isMovingDown;
        [SerializeField] private bool isWallSliding;
        [SerializeField] private bool isStomping;
        private Vector3 _baseScale;

        private static readonly int XKey = Animator.StringToHash("X");
        private static readonly int IsJumpKey = Animator.StringToHash("IsJump");
        private const float ColliderDisableDelay = 0.75f;
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

        public void OnHurt(float x)
        {
            Debug.Log($"x={x}, ogX={transform.position.x}");
            var blood = Instantiate(bloodVFX, transform.position, Quaternion.identity);
            var bloodX = x > transform.position.x ? -1f : 1f;
            blood.transform.localScale = new Vector3(bloodX, 1f, 1f);
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
            StartCoroutine(WaitForSeconds(ColliderDisableDelay));
        }

        private void Flip(float horizontal)
        {
            if (horizontal < 0)
            {
                transform.localScale = new Vector3(-_baseScale.x, _baseScale.y, _baseScale.z);
                dustVFX.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (horizontal > 0)
            {
                transform.localScale = _baseScale;
                dustVFX.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        private void Jump()
        {
            if (!isGrounded)
            {
                if (canDoubleJump)
                {
                    DoJump();
                    canDoubleJump = false;
                }
            }
            else
            {
                if (isGrounded)
                {
                    DoJump();
                }
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
                canDoubleJump = true;
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
