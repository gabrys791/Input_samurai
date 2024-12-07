using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public TextMeshProUGUI score;
    public TextMeshProUGUI health;
    public TextMeshProUGUI controls;
    void Update()
    {
        UpdateHealthText();
        UpdateScoreText();
    }
    private void Start()
    {
        UpdateControlsText();
    }
    private void UpdateHealthText()
    {
        health.text = "Health: " + playerMovement.GetComponent<HealthManager>().currentHealth;
    }

    private void UpdateScoreText()
    {
        score.text = "Score: " + GameManager.score.ToString();
    }
    private void UpdateControlsText()
    {
        controls.text = "Current input: " + playerMovement.controlScheme;
    }
}
