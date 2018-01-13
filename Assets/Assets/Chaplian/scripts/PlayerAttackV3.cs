using UnityEngine;
using System.Collections;

public class PlayerAttackV3 : MonoBehaviour
{
    PlayerInput plInput;
    PlayerMovementV002 plMovement;
    AudioSource audio;
    Animator anim;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem ShellCasing;
    public ParticleSystem Smoke;
    public float fireRate = 2;
    public float heavyFireRate = 2;

    public string[] comboParams;
    private int comboIndex = 0;
    public string[] heavyComboParams;
    private int heavyComboIndex = 0;
    private Animator animator;
    private float resetTimer;
    private float heavyResetTimer;

    public GameObject damageCollider;
    public GameObject PowerDamageCollider;
    public GameObject HeavyDamageCollider;

    public GameObject shot;
    public Transform shotSpawn;
    public float fxFireRate;

    private float nextFire;

    bool canShoot = true;
    public bool canPowerAttack = true;
    public bool canHeavyAttack = true;

    public AudioClip gunshot;
    public AudioClip weaponSwipe;
    public AudioClip heavyWeaponSwipe;
    public AudioClip powerAttackAudio;
    public AudioClip hit;

    void Start ()
    {
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
            comboParams = new string[] { "Attack1", "Attack2", "Attack3", "Attack4", "Attack5", "Attack6", "Attack7", "Attack8", };

        if (heavyComboParams == null || (heavyComboParams != null && heavyComboParams.Length == 0))
            heavyComboParams = new string[] { "HeavyAttack1", "HeavyAttack2", "HeavyAttack3", };

        animator = GetComponent<Animator>();

        plInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        plMovement = GetComponent<PlayerMovementV002>();
        audio = GetComponent<AudioSource>();

        //comboR = new WaitForSeconds(comboRate);

        damageCollider.SetActive(false);
        PowerDamageCollider.SetActive(false);
    }
    void Update()
    {
        //Fast attack combo
        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length)
        {            
            Debug.Log(comboParams[comboIndex] + " triggered");
            animator.SetTrigger(comboParams[comboIndex]);            
            comboIndex++;
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

        //Power attack
        if (canPowerAttack)
        {
            if (Input.GetButtonDown("Submit"))
            {
                anim.SetTrigger("PowerAttack");
                plMovement.canMove = false;
                plMovement.canAimMove = false;
                canShoot = false;
                canPowerAttack = false;
                canHeavyAttack = false;
                StartCoroutine("PowerAttackStart");
                StartCoroutine("HeavyAttackDisabled");
            }
        }

        //Heavy attack
        if (canHeavyAttack)
        {
            if (Input.GetButtonDown("Fire4"))
            {
                animator.SetTrigger("HeavyAttack1");
                plMovement.canMove = false;
                plMovement.canAimMove = false;
                canShoot = false;
                canPowerAttack = false;
                canHeavyAttack = false;
                StartCoroutine("HeavyAttackStart1");
                StartCoroutine("HeavyAttackDisabled");
            }
        }

        //Shooting attack
        if (canShoot)
        {
            if ((plInput.fire2) && Time.time > nextFire)
            {
                anim.SetTrigger("Shoot");
                nextFire = Time.time + fxFireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            }
            else
            {
                anim.ResetTrigger("Shoot");
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

    public void GunFireFX()
    {

        audio.clip = gunshot;
        audio.Play();
        MuzzleFlash.Emit(1);
        ShellCasing.Emit(1);
        Smoke.Emit(1);
    }
    
    public void OpenPowerDamageCollider()
    {
        PowerDamageCollider.SetActive(true);
    }

    public void ClosePowerDamageCollider()
    {
        PowerDamageCollider.SetActive(false);
    }
    public void OpenHeavyDamageCollider()
    {
        HeavyDamageCollider.SetActive(true);
    }

    public void CloseHeavyDamageCollider()
    {
        HeavyDamageCollider.SetActive(false);
    }

    public void FastMeleeAudio()
    {
        audio.clip = weaponSwipe;
        audio.Play();
    }

    public void HeavyAttackAudio()
    {
        audio.clip = heavyWeaponSwipe;
        audio.Play();
    }
    
    public void PowerAttackAudio()
    {
        audio.clip = powerAttackAudio;
        audio.Play();
    }
    public void playerHitAudio()
    {
        audio.clip = hit;
        audio.Play();
    }

    IEnumerator PowerAttackStart()
    {
        yield return new WaitForSeconds(2.1f);
        plMovement.canMove = true;
        plMovement.canAimMove = true;
        canShoot = true;
        canPowerAttack = true;

    }
    IEnumerator HeavyAttackStart1()
    {
        yield return new WaitForSeconds(2.8f);
        plMovement.canMove = true;
        plMovement.canAimMove = true;
        canShoot = true;
        canPowerAttack = true;
    }
    IEnumerator HeavyAttackDisabled()
    {
        yield return new WaitForSeconds(4f);
        canHeavyAttack = true;
    }
}

