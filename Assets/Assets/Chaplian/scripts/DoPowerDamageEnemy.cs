using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoPowerDamageEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStatsEnemy>())
        {
            other.GetComponent<CharacterStatsEnemy>().checkToApplyPowerDamage();
        }
    
    }
}
