using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public TextMeshProUGUI score;
    public Image healthBar;
    public TextMeshProUGUI controls;
    [SerializeField] private HealthManager healthManager;
    private float currenthealth;
    private float maxHealth;
    void Update()
    {
        UpdateHealthText();
        UpdateScoreText();
        currenthealth = healthManager.currentHealth;
    }
    private void Start()
    {
        UpdateControlsText();
        maxHealth = healthManager.currentHealth;
    }
    private void UpdateHealthText()
    {
        healthBar.rectTransform.localScale = new Vector3(currenthealth/maxHealth, 1f, 1f);
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
