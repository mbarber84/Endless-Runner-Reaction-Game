using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;

    private Vector3 moveVector;

    private float speed = 5.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12f;

    //Time in seconds for start animation (camera swoop)
    private float animationDuration = 3.0f;

    private float startTime;

    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;


        if (Time.time - startTime < animationDuration)
        {
            //Restricts movement till start animation is complete
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // x = left and right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

        if (transform.position.y < -5f)
            Death();

        //for mobile
        if (Input.GetMouseButton(0))
        {
            //do detect if we are pressing on the rightside
            if(Input.mousePosition.x > Screen.width / 2)
                moveVector.x = speed;
            else
                moveVector.x = -speed;
        }

        // y = up and down
        moveVector.y = verticalVelocity;

        // z = forwards and backwards
        moveVector.z = speed;


        controller.Move(moveVector * Time.deltaTime);
    }
    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

    //this is called when player (capsual is character controller) hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + controller.radius)
            Death();
    }

    private void Death()
    {
        isDead = true;
        GetComponent<Score> ().OnDeath();
    }
}
