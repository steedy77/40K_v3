using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoHeavyDamageEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStatsEnemy>())
        {
            other.GetComponent<CharacterStatsEnemy>().checkToApplyHeavyDamage();
        }
    
    }
}
