using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public InputActions inputActions;

    private Vector2 movementInput;
    private Player player;

    private void Start()
    {
        inputActions = GetComponent<InputActions>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        movementInput = inputActions.PlayerInput.Player.Movement.ReadValue<Vector2>();

        bool isMoving = movementInput.magnitude > 0.1f;
        animator.SetBool("IsWalking", isMoving);
        animator.SetFloat("MoveX", movementInput.x);
        animator.SetFloat("MoveY", movementInput.y);

        float moveSpeed = movementInput.magnitude;

        animator.SetFloat("MoveSpeed", moveSpeed);

        UpdatePlayerRotation();
    }

    private void UpdatePlayerRotation()
    {
        if (movementInput.x >= 1)
            player.transform.rotation = Quaternion.Euler(0, 90, 0);

        if (movementInput.x <= -1)
            player.transform.rotation = Quaternion.Euler(0, -90, 0);

        if (movementInput.y >= 1)
            player.transform.rotation = Quaternion.identity;

        if(movementInput.y <= -1)
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    //public void OnAttack(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        // Trigger the attack animation by setting the "isAttacking" parameter to true
    //        // You may want to play a sound or perform other attack-related actions here
    //    }
    //    else if (context.canceled)
    //    {
    //        // Reset the "isAttacking" parameter to false to stop the attack animation
    //    }
    //}
}
