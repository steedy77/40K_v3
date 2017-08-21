using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class PlayerControlScript : MonoBehaviour
{
	[System.NonSerialized]					
	public float meshMoveSpeed = 4.0f;
	
	[System.NonSerialized]
	public float animSpeed = 1.5f;              // a public setting for overall animator animation speed

    // If true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
    public bool limitDiagonalSpeed = true;

    private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2

	static int reloadState = Animator.StringToHash("Layer2.Reload");				// and are used to check state for various actions to occur

	static int switchWeaponState = Animator.StringToHash("Layer2.WeaponSwap");
	

	void Start ()
	{
		// initialising reference variables
		anim = GetComponent<Animator>();					  					
		if(anim.layerCount ==2)
			anim.SetLayerWeight(1, 1);
	}
	
	void OnAnimatorMove() //Tells Unity that root motion is handled by the script
	{
		if(anim)
		{
			Vector3 newPosition = transform.position;
			newPosition.z += anim.GetFloat("Speed")* meshMoveSpeed * Time.deltaTime;
			newPosition.x += anim.GetFloat("Direction") * meshMoveSpeed * Time.deltaTime;
			transform.position = newPosition;
		}
	}
	
	
	void FixedUpdate ()
	{
		float v = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		float h = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
        bool  a = Input.GetButtonDown("Fire1");              // setup a variable as melee
        bool  s = Input.GetButtonDown("Fire2");              // setup s variable as shoot

        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
        float inputModifyFactor = (h != 0.0f && v != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;

        anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis	
        anim.SetBool("Attacking", a);                      // set our animator's float parameter 'Attacking' equal to  fire1
        anim.SetBool("Shooting", s);                       // set our animator's float parameter 'Shooting' equal to  fire2
        anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		//anim.SetLookAtWeight(lookWeight);					// set the Look At Weight - amount to use look at IK vs using the head's animation
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation
		
		//Controls the movement speed
		if(v <= 0.0f)
		{
			meshMoveSpeed = 6;	
		}
		else
		{
			meshMoveSpeed = 6;
		}
		
		if(anim.layerCount ==2)
		{
			layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);	// set our layer2CurrentState variable to the current state of the second Layer (1) of animation
		}
		//Melee attack
		if(Input.GetButtonDown("Fire1"))
		{
			anim.SetBool("Attacking", true);
		}
		else
		{
			anim.SetBool("Attacking", false);
		}
        //Shooting attack
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("Shooting", true);
        }
        else
        {
            anim.SetBool("Shooting", false);

        }
	}
}
