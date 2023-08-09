using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

[RequireComponent(typeof(CharacterController))]
public class Player : NetworkBehaviour
{
    [Range(0f,100f)]
    public float moveSpeed = 5f;
    public float damping = 5f;
    public bool canMove = true;
    [Header("Health")]
    [Space]
    public NetworkVariable<float> currentHealth = new NetworkVariable<float>();
    public float maxHealth;

    //Private Fields
    private CharacterController characterController;
    private Animator animator;
    private CameraBase cameraController;
    private Vector2 movementInput;
    private Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraController = FindAnyObjectByType<CameraBase>();

        if (IsLocalPlayer)
        {
            cameraController.target = gameObject.transform;
        }

        if (IsServer)
        {
            currentHealth.Value = maxHealth;
        }
    }

    private void Update()
    {
        if (!canMove || !IsLocalPlayer)
        {
            return;
        }

        Move();
        movementInput = Vector2.zero;
    }

    public void Move()
    {

        movementInput = InputActions.Instance.PlayerInput.Player.Movement.ReadValue<Vector2>();

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);

        velocity = Vector3.Lerp(velocity, movement * moveSpeed, damping * Time.deltaTime);

        characterController.Move(velocity * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (!IsServer)
        {
            return;
        }

        animator.SetBool("Hurt", true);
        currentHealth.Value -= damage;
        animator.SetBool("Hurt", false);
        UIManager.Instance.UpdatePlayerHealthBars();

        if (currentHealth.Value <= 0)
        {
            currentHealth.Value = 0;
            DieClientRpc();
        }
    }

    [ClientRpc]
    private void DieClientRpc()
    {
        if (IsLocalPlayer)
        {
            animator.SetTrigger("Dead");
            transform.position = Vector3.zero;
        }

        //Incase you want to respawn uncomment this
        //currentHealth.Value = maxHealth;
    }
}
