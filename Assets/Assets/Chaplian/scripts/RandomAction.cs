using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAction : MonoBehaviour {

    private int numActions;

    // Use this for initialization
    void Start () {
        Animator anim = GetComponent <Animator>();
        int randomValue = Random.Range(0, numActions);
        anim.SetFloat("Random", (float)randomValue);
		
	}
	
}
