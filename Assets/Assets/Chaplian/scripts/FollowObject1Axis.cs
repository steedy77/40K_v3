using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject1Axis : MonoBehaviour {

    public Transform target;
    float originalX;
    float originalY;

    void Start ()
    {
        originalX = transform.position.x;
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(originalX, originalY, target.position.z);
    }
}

