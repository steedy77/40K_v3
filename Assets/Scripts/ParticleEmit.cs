using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmit : MonoBehaviour {

    public ParticleSystem AxeSwipe;
	
	
	// Update is called once per frame
	void Update () {
    AxeSwipe.Emit(300);
}
}
