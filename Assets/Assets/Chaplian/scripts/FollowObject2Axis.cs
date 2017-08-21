using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject2Axis : MonoBehaviour {

    public Transform target;
    float originalX;

    void Start ()
    {
        originalX = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(originalX, target.position.y, target.position.z);
    }
}

