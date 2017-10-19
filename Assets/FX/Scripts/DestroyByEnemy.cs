using UnityEngine;
using System.Collections;

public class DestroyByEnemy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bolt")
        {
            Destroy(other.gameObject);
        }
    }
}