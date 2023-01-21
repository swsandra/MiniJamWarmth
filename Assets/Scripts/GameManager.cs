using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Timer")]
    [SerializeField] int years;
    int score;

    public int Score {
        get => score;
        set {
            score = value;
            // UIManager.Instance.UpdateUIScore(score);
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
