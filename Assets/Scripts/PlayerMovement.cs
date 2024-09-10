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
    [SerializeField] private Camera cam;

    private Vector2 movementDir;
    private Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
         
    }

    // Update method called at fixed intervals (for physics & movement)
    private void FixedUpdate()
    {
        //moving player
        playerRB.MovePosition(playerRB.position + movementDir * moveSpeed * Time.fixedDeltaTime);
        
        //rotating player
        Vector2 lookDir = mousePos - playerRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        playerRB.rotation = angle;
    }

    //Method that updates the movement direction of the player
    public void GetMovement(InputAction.CallbackContext input)
    {
        movementDir = input.ReadValue<Vector2>();
    }

    //Method that gets the direction the player should be facing
    public void GetDirection(InputAction.CallbackContext input)
    {
        mousePos = cam.ScreenToWorldPoint(input.ReadValue<Vector2>());
    }
}
