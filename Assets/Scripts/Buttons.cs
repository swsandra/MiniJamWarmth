using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject controlsScreen;

    public void MainMenu() {
        SceneManager.LoadScene("Title");
    }

    public void Play() {
        SceneManager.LoadScene("Game");
    }

    public void ShowControls(){
        controlsScreen.SetActive(true);
    }
}
