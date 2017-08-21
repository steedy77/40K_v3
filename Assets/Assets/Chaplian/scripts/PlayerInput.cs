using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public float horizontal;
    public float vertical;
    public bool fire1;
    public bool fire2;


    void FixedUpdate()
    {
        this.horizontal = Input.GetAxis("Horizontal");
        this.vertical = Input.GetAxis("Vertical");
        this.fire1 = Input.GetButton("Fire1");
        this.fire2 = Input.GetButton("Fire2");
    }

}

