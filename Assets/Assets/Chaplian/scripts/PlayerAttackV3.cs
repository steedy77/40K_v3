using UnityEngine;
using System.Collections;

public class PlayerAttackV3 : MonoBehaviour
{
    PlayerInput plInput;
    PlayerMovement plMovement;
    Animator anim;

    public float fireRate = 2;

    public string[] comboParams;
    private int comboIndex = 0;
    private Animator animator;
    private float resetTimer;

    public GameObject damageCollider;
    public GameObject damageCollider2;

    void Start ()
    {
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
            comboParams = new string[] { "Attack1", "Attack2", "Attack3", "Attack4", "Attack5", "Attack6", };

        animator = GetComponent<Animator>();

        plInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        plMovement = GetComponent<PlayerMovement>();

        //comboR = new WaitForSeconds(comboRate);

        damageCollider.SetActive(false);
        damageCollider2.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length)
        {
            //plMovement.canMove = false;
            Debug.Log(comboParams[comboIndex] + " triggered");
            animator.SetTrigger(comboParams[comboIndex]);

            // If combo must not loop
            comboIndex++;

            // If combo can loop
            // comboIndex = (comboIndex + 1) % comboParams.Length ;

            resetTimer = 0f;
        }
        // Reset combo if the user has not clicked quickly enough
        if (comboIndex > 0)
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > fireRate)
            {
                animator.SetTrigger("Reset");
                comboIndex = 0;
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

