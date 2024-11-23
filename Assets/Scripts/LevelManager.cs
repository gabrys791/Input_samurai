using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int index;
    public string levelName;
    private ArtefactSpawner artefact;
    private void Start()
    {
        artefact = FindObjectOfType<ArtefactSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && artefact.collected == artefact.numberOfArtifacts)
        {          
            SceneManager.LoadScene(index);
        }
    }
}
