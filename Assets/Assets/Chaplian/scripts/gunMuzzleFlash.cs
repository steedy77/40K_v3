using UnityEngine;
using System.Collections;

public class gunMuzzleFlash : MonoBehaviour {

    PlayerInput plInput;
    Animator anim;
    

    void Start ()
    {
        plInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        if (plInput.fire2)
        {
            anim.SetBool("Shoot", true);            
        }
        else
        {
            anim.SetBool("Shoot", false);
        }
    }
    

}

