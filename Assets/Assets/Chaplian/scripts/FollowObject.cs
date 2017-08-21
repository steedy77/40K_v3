using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform target;
    public float speed;


    private void FixedUpdate()
    {
        Vector3 pos = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.position = pos;
    }
}

