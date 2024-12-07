using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void SetWASD()
    {
        PlayerPrefs.SetString("ControlScheme", "WASD");
        PlayerPrefs.Save();
    }
    public void SetESDF()
    {
        PlayerPrefs.SetString("ControlScheme", "ESDF");
        PlayerPrefs.Save();
    }
    public void SetSDFSpace()
    {
        PlayerPrefs.SetString("ControlScheme", "SDFSpace");
        PlayerPrefs.Save();
    }
    public void Set5678()
    {
        PlayerPrefs.SetString("ControlScheme", "5678");
        PlayerPrefs.Save();
    }
    public void SetDCSA()
    {
        PlayerPrefs.SetString("ControlScheme", "DCSA");
        PlayerPrefs.Save();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }

}
