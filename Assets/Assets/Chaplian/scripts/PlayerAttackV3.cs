using UnityEngine;
using System.Collections;

public class PlayerAttackV3 : MonoBehaviour
{
    PlayerInput plInput;
    PlayerMovementV002 plMovement;
    Animator anim;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem ShellCasing;
    public ParticleSystem Smoke;
    public float fireRate = 2;

    public string[] comboParams;
    private int comboIndex = 0;
    private Animator animator;
    private float resetTimer;

    public GameObject damageCollider;
    public GameObject damageCollider2;
    public GameObject PowerDamageCollider;

    public GameObject shot;
    public Transform shotSpawn;
    public float fxFireRate;

    private float nextFire;

    bool canShoot = true;
    bool canPowerAttack = true;

    void Start ()
    {
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
            comboParams = new string[] { "Attack1", "Attack2", "Attack3", "Attack4", "Attack5", "Attack6", "Attack7", "Attack8", };

        animator = GetComponent<Animator>();

        plInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        plMovement = GetComponent<PlayerMovementV002>();

        //comboR = new WaitForSeconds(comboRate);

        damageCollider.SetActive(false);
        damageCollider2.SetActive(false);
        PowerDamageCollider.SetActive(false);
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
                comboIndex = 0;
                animator.SetTrigger("Reset");
            }
            else
            {
                animator.ResetTrigger("Reset");
            }
        }

        else
        {
            animator.ResetTrigger("Attack1");
            animator.ResetTrigger("Attack2");
            animator.ResetTrigger("Attack3");
            animator.ResetTrigger("Attack4");
            animator.ResetTrigger("Attack5");
            animator.ResetTrigger("Attack6");
            animator.ResetTrigger("Attack7");
            animator.ResetTrigger("Attack8");
        }

        if (canPowerAttack)
        {
            if (Input.GetButtonDown("Submit"))
            {
                anim.SetBool("PowerAttack", true);
                plMovement.canMove = false;
                canShoot = false;
                canPowerAttack = false;
                StartCoroutine("PowerAttackStart");
            }
            else
            {
                anim.SetBool("PowerAttack", false);
            }
        }

        if (canShoot)
        {
            if ((plInput.fire2) && Time.time > nextFire)
            {
                anim.SetBool("Shoot", true);
                nextFire = Time.time + fxFireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            }
            else
            {
                anim.SetBool("Shoot", false);
            }
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
        MuzzleFlash.Emit(1);
        ShellCasing.Emit(1);
        Smoke.Emit(1);
    }
    
    public void CloseDamageCollider2()
    {
        damageCollider2.SetActive(false);
    }

    public void OpenPowerDamageCollider()
    {
        PowerDamageCollider.SetActive(true);
    }

    public void ClosePowerDamageCollider()
    {
        PowerDamageCollider.SetActive(false);
    }
    IEnumerator PowerAttackStart()
    {
        yield return new WaitForSeconds(3);
        plMovement.canMove = true;
        canShoot = true;
        canPowerAttack = true;

    }
}

