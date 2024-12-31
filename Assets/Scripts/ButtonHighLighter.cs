using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ButtonHighLighter : MonoBehaviour
{
    public GameObject highLight;

    public Button button;
    public void mouseEnter()
    {
        if(button.IsInteractable())
        {
            highLight.SetActive(true);
        }
    }
}
