using UnityEngine;
using System.Collections;

public class TriggerZoom : MonoBehaviour
{
    public GameObject cam1, cam2;

    void Start()
    {
        {
            cam1.SetActive(true);
            cam2.SetActive(false);

        }
    }
        void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
        //else
        //{
        //    cam1.SetActive(true);
        //    cam2.SetActive(false);
        //}
    }



    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        //{
        //    cam1.SetActive(false);
        //    cam2.SetActive(true);
        //}
        //else
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
    }
}

