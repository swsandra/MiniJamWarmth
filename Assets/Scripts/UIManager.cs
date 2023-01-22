using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Score")]
    [SerializeField] TMP_Text scoreText;
    [Header("Timer")]
    [SerializeField] TMP_Text yearsText;
    [Header("World")]
    [SerializeField] Image burningPlanetImg;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void UpdateUIScore(int newScore) {
        scoreText.text = newScore.ToString();
    }

    public void UpdateUIYears(int newYears) {
        yearsText.text = newYears.ToString();
    }

    public void UpdateUIPlanet(float newAlpha) {
        burningPlanetImg.color = new Color(burningPlanetImg.color.r, burningPlanetImg.color.g, burningPlanetImg.color.b, newAlpha);
    }
}
