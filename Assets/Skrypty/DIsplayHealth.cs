using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DIsplayHealth : MonoBehaviour
{
    [SerializeField] private HealthManager playerHealth;
    public Text healthCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthCount.text = "Health " + playerHealth.currentHealth.ToString();    
    }
}
