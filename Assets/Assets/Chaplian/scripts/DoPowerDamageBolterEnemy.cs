using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoPowerDamageBolterEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStatsBolterEnemy>())
        {
            other.GetComponent<CharacterStatsBolterEnemy>().checkToApplyPowerDamage();
        }
    
    }
}
