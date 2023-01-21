using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Score")]
    [SerializeField] int score;
    // TODO: add percentages for each final
    [Header("Timer")]
    [SerializeField] int years;
    [SerializeField] int time;
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

    public int Time {
        get => time;
        set {
            time = value;
            // UIManager.instance.UpdateUITime(time);
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        totalFactories = 0;
        score = 0;
    }

    private void Start() {
        StartCoroutine(CountDownRoutine());
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

    IEnumerator CountDownRoutine() {
        while(time >= 0) {
            yield return new WaitForSeconds(1);
            Time--;
        }
        GameOver();
    }

    [ContextMenu("Game Over")]
    void GameOver() {
        Debug.Log("Game Over, pick final");
    }
}
