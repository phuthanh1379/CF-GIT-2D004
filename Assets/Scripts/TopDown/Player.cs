using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace TopDown
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float speed;

        [Header("Attack")]
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask enemyLayerMask;
        [SerializeField] private Transform downAttackPoint;
        [SerializeField] private Transform leftAttackPoint;
        [SerializeField] private Transform rightAttackPoint;
        [SerializeField] private Transform upAttackPoint;

        private const string AttackKey = "Attack";
        private const string IsMovingKey = "IsMoving";
        private const string XKey = "X";
        private const string YKey = "Y";

        private Vector2 _input = Vector2.zero;
        private bool _isAttacking;
        private bool _isMovable;

        public event Action<string> TalkToNPC;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null)
            {
                return;
            }

            if (collision.CompareTag("NeutralNPC"))
            {
                StartTalkToNPC(collision);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(downAttackPoint.position, attackRadius);
            Gizmos.DrawWireSphere(leftAttackPoint.position, attackRadius);
            Gizmos.DrawWireSphere(rightAttackPoint.position, attackRadius);
            Gizmos.DrawWireSphere(upAttackPoint.position, attackRadius);
        }

        private void Awake()
        {
            _isMovable = true;
        }

        private void Update()
        {
            Move();
            Attack();
        }

        public void SetMovable(bool movable) => _isMovable = movable;

        private void StartTalkToNPC(Collider2D collision)
        {
            if (!collision.TryGetComponent<NPC>(out var npc))
            {
                return;
            }

            _isMovable = false;
            TalkToNPC?.Invoke(npc.DialogueContent);
        }

        private void Move()
        {
            if (!_isMovable)
            {
                return;
            }

            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var isMoving = horizontal != 0 || vertical != 0;
            if (isMoving)
            {
                _input = new Vector2(horizontal, vertical);
            }

            transform.Translate(speed * Time.deltaTime * new Vector2(horizontal, vertical));
            animator.SetFloat(XKey, _input.x);
            animator.SetFloat(YKey, _input.y);
            animator.SetBool(IsMovingKey, isMoving);
        }

        private void Attack()
        {
            // Left click
            if (Input.GetMouseButtonDown(0) && !_isAttacking)
            {
                _isAttacking = true;
                animator.SetTrigger(AttackKey);

                if (_input.y >= 1f)
                {
                    CheckAttack(upAttackPoint);
                }
                else if (_input.y <= -1f)
                {
                    CheckAttack(downAttackPoint);
                }
                else if (_input.x >= 1f)
                {
                    CheckAttack(rightAttackPoint);
                }
                else if (_input.x <= -1f)
                {
                    CheckAttack(leftAttackPoint);
                }
            }
        }

        private void CheckAttack(Transform attackPoint)
        {
            var collider = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayerMask);
            if (collider != null && collider.GetComponent<Enemy>() != null)
            {
                collider.GetComponent<Enemy>().OnHit();
            }
        }

        private void OnCompleteAttack()
        {
            _isAttacking = false;
        }
    }
}
