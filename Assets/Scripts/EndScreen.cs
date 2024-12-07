using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI controlScheme;
    public TextMeshProUGUI timeCount;
    void Start()
    {
        
    }
    void Update()
    {     
        UpdateTimeCount();
        UpdateControlScheme();
    }
    private void UpdateTimeCount()
    {
        timeCount.text = "Time: " + GameManager.time.ToString();
    }
    private void UpdateControlScheme()
    {
        controlScheme.text = "Control scheme used: " + GameManager.cs;
    }
}
