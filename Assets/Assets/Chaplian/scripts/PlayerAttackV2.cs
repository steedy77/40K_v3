using UnityEngine;
using System.Collections;

public class PlayerAttackV2 : MonoBehaviour {

    PlayerInput plInput;
    PlayerMovement plMovement;
    Animator anim;

    //public float comboRate = .5f;

    //bool attack = false;
    //public double attacktime = 0.0;

    WaitForSeconds comboR;
    public GameObject damageCollider;
    public GameObject damageCollider2;

    void Start ()
    {
        plInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        plMovement = GetComponent<PlayerMovement>();
                
        //comboR = new WaitForSeconds(comboRate);
                
        damageCollider.SetActive(false);
        damageCollider2.SetActive(false);

    }

    void FixedUpdate()
    {
        {
            if (plInput.fire1) 
            {
                anim.SetBool("Attack", true);
                //attack = true;
                //attacktime = 2.0;
            }
            else
            {
                anim.SetBool("Attack", false);
            }
            
            
        }

        if (plInput.fire2)
        {
            anim.SetBool("Shoot", true);
            plMovement.canMove = false;
        }
        else
        {
            anim.SetBool("Shoot", false);
        }
    }

    

    public void OpenDamageCollider()
    {
        damageCollider.SetActive(true);
    }

    public void CloseDamageCollider()
    {
        damageCollider.SetActive(false);
    }

    public void OpenDamageCollider2()
    {
        damageCollider2.SetActive(true);
    }

    public void CloseDamageCollider2()
    {
        damageCollider2.SetActive(false);
    }
}

