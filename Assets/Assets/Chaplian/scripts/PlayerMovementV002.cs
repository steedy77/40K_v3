using UnityEngine;
using System.Collections;
using UnityEngine;

public class PlayerMovementV002 : MonoBehaviour
{

   
    float speed = 1.6f;            // The speed that the player will move at.
    float sprintSpeed = 5f;
    float runSpeed = 1.6f;
    float aimSpeed = 0.001f;

    float xVelAdj = 0;
    float zVelAdj = 0;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    private object direction;
    private Quaternion rotation;

    void Awake()
    {
        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    
    //HS changed from fixed to update
    void Update()
    {
        // Store the input axes.
        float v = Input.GetAxis("Horizontal");              // setup h variable as our horizontal input axis
        float h = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis

        xVelAdj = Input.GetAxis("Vertical");
        zVelAdj = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().velocity = new Vector3(4 * xVelAdj, 0, 4 * zVelAdj);

        
        // Move the player around the scene.
        Move(h, v);
        
        // Animate the player.
        Animating(h, v);

        //sprinting
        if (Input.GetButton("Fire3"))
        {
            speed = sprintSpeed;
            anim.SetBool("SprintMode", true);
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            speed = runSpeed;
            anim.SetBool("SprintMode", false);
        }
        if (Input.GetButton("Jump"))
        {
            speed = aimSpeed;
            anim.SetBool("AimMode", true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            speed = runSpeed;
            anim.SetBool("AimMode", false);
        }

    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);
                
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
        
        
        // Tell the animator whether or not the player is walking.
        anim.SetFloat("speed", v);                   // set our animator's float parameter 'Speed' equal to the vertical input axis	
        anim.SetFloat("direction", h);                   // set our animator's float parameter 'Direction' equal to the horizontal input axis                        			


        //rotate the player
        if (xVelAdj != 0f || zVelAdj != 0f)
        {

            playerRigidbody.MoveRotation(Quaternion.LookRotation(movement));
            Debug.Log(xVelAdj + ","+ zVelAdj);
        }


        

    }
    

}