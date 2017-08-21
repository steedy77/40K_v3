using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    PlayerInput plInput;
    PlayerMovement plMovement;
    Animator anim;

    public float comboRate = .5f;

    WaitForSeconds comboR;
    public GameObject damageCollider;
    public GameObject damageCollider2;

    void Start ()
    {
        plInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        plMovement = GetComponent<PlayerMovement>();

        comboR = new WaitForSeconds(comboRate);

        damageCollider.SetActive(false);
        damageCollider2.SetActive(false);
    }

    void FixedUpdate()
    {
        if (plInput.fire1)
        {
            anim.SetBool("Attack", true);
            plMovement.canMove = false;
            StartCoroutine("CloseAttack");
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

    IEnumerator CloseAttack()
    {
        yield return comboR;
        anim.SetBool("Attack", false);
    }

    public void OpenDamageCollider()
    {
        damageCollider.SetActive(true);
        damageCollider2.SetActive(true);
    }

    public void CloseDamageCollider()
    {
        damageCollider.SetActive(false);
        damageCollider2.SetActive(false);
    }

}

