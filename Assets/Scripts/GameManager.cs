using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Transform player;
    [Header("Score")]
    [SerializeField] int score;
    [SerializeField] float goodEndingPercentage;
    [SerializeField] float normalEndingPercentage;
    [Header("Timer")]
    [SerializeField] int years;
    [SerializeField] int time;
    [SerializeField] float yearsPerSecond;
    [Header("World")]
    [SerializeField] int totalFactories;
    [SerializeField] float redAlpha;

    public int Score {
        get => score;
        set {
            score = value;
            UIManager.instance.UpdateUIScore(score);
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

    public float RedAlpha {
        get => redAlpha;
        set {
            redAlpha = value;
            UIManager.instance.UpdateUIPlanet(redAlpha);
        }
    }

    public int Time {
        get => time;
        set {
            time = value;
        }
    }

    public int Years {
        get => years;
        set {
            years = value;
            UIManager.instance.UpdateUIYears(years);
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
        UIManager.instance.UpdateUIScore(score);
        UIManager.instance.UpdateUIYears(years);

        yearsPerSecond = (float)time/(float)years;
        StartCoroutine(CountDownRoutine());
        StartCoroutine(CountDownYearsRoutine());
    }

    public void UpdateRedAlpha() {
        if (totalFactories <= 0) return;
        float value = totalFactories - score;
        float max = totalFactories;
        float min = 0;
        RedAlpha = (value-min)/(max-min);
    }

    IEnumerator CountDownRoutine() {
        while(time > 0) {
            yield return new WaitForSeconds(1);
            Time--;
        }
        GameOver();
    }

    IEnumerator CountDownYearsRoutine() {
        while(years > 0) {
            yield return new WaitForSeconds(yearsPerSecond);
            Years--;
        }
    }

    [ContextMenu("Game Over")]
    void GameOver() {
        player.GetComponent<PlayerController>().GameOver();
        float completion = (1-RedAlpha);
        if (completion >= goodEndingPercentage/100){
            Debug.Log("Good ending: "+completion);
        } else if (completion >= normalEndingPercentage/100){
            Debug.Log("Normal ending: "+completion);
        } else {
            Debug.Log("Bad ending: "+completion);
        }
    }
}
