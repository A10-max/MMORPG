using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : NetworkBehaviour
{
    public Animator animator;
    public float attackCooldown = 1f;
    public float attackDamage = 20;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    private bool isAttacking = false;
    private float lastAttackTime;
    private Player player;
    private InputAction attackAction;

    private void Start()
    {
        attackAction = InputActions.Instance.PlayerInput.Player.Attack;
        attackAction.performed += OnAttackPerformed;
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }


    private void Update()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        if (isAttacking == true)
        {
            player.canMove = false;
        }
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (!isAttacking && Time.time - lastAttackTime > attackCooldown)
        {
            Debug.Log("Called");
            AttackServerRpc();
        }
    }

    [ServerRpc]
    private void AttackServerRpc()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        animator.SetBool("CanAttack", false);

        // Use raycasting or trigger colliders to detect enemies in front of the player
        // and deal damage to them
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }

    public void OnAttackAnimationEnd()
    {
        if(IsLocalPlayer)
        {
            // This method is called from the animation event at the end of the attack animation
            isAttacking = false;
            lastAttackTime = Time.time;
            player.canMove = true;

            animator.SetBool("IsAttacking", false);
            animator.SetBool("CanAttack", true);
        }
    }
}
