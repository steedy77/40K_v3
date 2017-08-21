using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStatsEnemy>())
        {
            other.GetComponent<CharacterStatsEnemy>().checkToApplyDamage();
        }
    
    }
}
