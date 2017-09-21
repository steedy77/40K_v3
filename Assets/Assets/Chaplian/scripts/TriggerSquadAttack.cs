using UnityEngine;
using System.Collections;

public class TriggerSquadAttack : MonoBehaviour
{
    Animator anim;
    bolterSquadAttack bsa;


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }
}

