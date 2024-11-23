using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefact : MonoBehaviour
{
    // Start is called before the first frame update
    ArtefactSpawner artefactSpawner;

    private void Start()
    {
        artefactSpawner = FindObjectOfType<ArtefactSpawner>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            artefactSpawner.CollectArtefact(this.gameObject);
        }
    }
}
