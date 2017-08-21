using UnityEngine;
using System.Collections;

public class TriggerZoom : MonoBehaviour
{
    Animator anim;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            anim.SetBool("CameraZoom", true);
        }
    }
}

