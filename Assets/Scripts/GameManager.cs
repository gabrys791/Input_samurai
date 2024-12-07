using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score;
    public static float time;
    public static string cs;
    public PlayerMovement playerMovement;
    void Start()
    {
        score = 0;
        time = 0;
        cs = PlayerPrefs.GetString("ControlScheme", playerMovement.controlScheme);
    }
    public static void InitGame()
    {
        score = 0;
    }
    public static void AddScore(int points)
    {
        score += points; 
    }
    private void Update()
    {
        time += Time.deltaTime;
    }
}
