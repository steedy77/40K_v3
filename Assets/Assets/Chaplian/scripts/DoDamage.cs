using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStats>())
        {
            other.GetComponent<CharacterStats>().checkToApplyDamage();
        }
    
    }
}
