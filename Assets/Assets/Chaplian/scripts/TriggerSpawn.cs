using UnityEngine;
using System.Collections;

public class TriggerSpawn : MonoBehaviour {

    GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerInput>())
        {
            gm.spawnEnemiesNow = true;
            Destroy(gameObject);
        }
    }
}

