using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArtefactSpawner : MonoBehaviour
{
    public List<Transform> positions = new List<Transform>();
    public GameObject prefab;
    public int numberOfArtifacts;
    private List<Transform> transforms = new List<Transform>();
    public int collected = 0;
    private List<GameObject> artefacts = new List<GameObject>();

    void Start()
    {
        List<Transform> list = new List<Transform>(positions);
        for(int i = 0; i< numberOfArtifacts; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            Transform position = list[randomIndex];
            Vector3 vector3 = position.position;
            GameObject artifact = Instantiate(prefab, vector3, Quaternion.identity);
            artefacts.Add(artifact);
            list.RemoveAt(randomIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CollectArtefact(GameObject artefact)
    {
        collected++;
        artefacts.Remove(artefact);
        Destroy(artefact);
    }

}
