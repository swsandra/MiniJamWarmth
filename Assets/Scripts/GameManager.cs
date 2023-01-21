using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int score;
    [Header("Timer")]
    [SerializeField] int years;
    [Header("World")]
    [SerializeField] int totalFactories;
    [SerializeField] float redAlpha;

    public int Score {
        get => score;
        set {
            score = value;
            // UIManager.Instance.UpdateUIScore(score);
            UpdateRedAlpha();
        }
    }

    public int TotalFactories {
        get => totalFactories;
        set {
            totalFactories = value;
            UpdateRedAlpha();
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        totalFactories = 0;
        score = 0;
    }

    public void UpdateRedAlpha() {
        if (totalFactories <= 0) return;
        float value = totalFactories - score;
        float max = totalFactories;
        float min = 0;
        float newAlpha = (value-min)/(max-min);
        Debug.Log("Alpha: "+newAlpha);
        // TODO: UIManager update alpha
    }
}
