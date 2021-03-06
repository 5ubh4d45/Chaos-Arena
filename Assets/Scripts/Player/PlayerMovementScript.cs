using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    
    public Rigidbody2D rb;
    public Camera cam;
    
    Vector2 movement;
    Vector2 mousePos;
    
    void Update(){
        
        ProcessInputs();
    }

    private void FixedUpdate() {
        
        MovePlayer();
    }

    private void ProcessInputs(){
        
        //Gethering input for Player MOvement X & Y axis
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized;
        
        //Gathering mousepointer position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MovePlayer(){
        //chnaging positions according to input
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //Finding Angle of rotation from player to mouse
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }



}
