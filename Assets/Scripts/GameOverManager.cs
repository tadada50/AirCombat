using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        scoreKeeper = ScoreKeeper.GetInstance();
    }

    void Start()
    {
        
        GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = "You Scored\n" + scoreKeeper.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
