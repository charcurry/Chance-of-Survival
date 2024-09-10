using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Object references")]
    [SerializeField] private Rigidbody2D playerRB;

    private Vector2 movementDir;

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update method called at fixed intervals (for physics)
    private void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movementDir * moveSpeed * Time.fixedDeltaTime);
    }

    //Method that updates the movement direction of the player
    public void GetMovement(InputAction.CallbackContext input)
    {
        movementDir = input.ReadValue<Vector2>();
    }
}
