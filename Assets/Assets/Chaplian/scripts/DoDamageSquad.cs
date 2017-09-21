using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageSquad : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStatsBolterSquad>())
        {
            other.GetComponent<CharacterStatsBolterSquad>().checkToApplyDamage();
        }
    
    }
}
