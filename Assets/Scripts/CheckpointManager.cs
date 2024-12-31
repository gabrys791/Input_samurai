using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Respawn respawn;
    private BoxCollider2D boxCollider2D;
    private Respawn[] respawns;
    void Awake()
    {
        respawns = GameObject.FindObjectsOfType<Respawn>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach(Respawn respawn in respawns)
            {
                respawn.respawnPoint = this.gameObject;
            }
            boxCollider2D.enabled = false;
        }
    }
}
