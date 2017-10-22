using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageBolterEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStatsBolterEnemy>())
        {
            other.GetComponent<CharacterStatsBolterEnemy>().checkToApplyDamage();
        }
    
    }
}
