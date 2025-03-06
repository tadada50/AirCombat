using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider healthSlider;
    Health playerHealth;
    ScoreKeeper scoreKeeper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        scoreKeeper.OnScoreChange += ScoreChangeHandler;
        playerHealth = FindFirstObjectByType<Player>().GetComponent<Health>();
        playerHealth.OnHealthChange += HealthChangeHandler;
        healthSlider.maxValue = playerHealth.GetHealth();
        healthSlider.value = playerHealth.GetHealth();
    }
    void ScoreChangeHandler(int score){
        // TextMeshProUGUI scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        if(scoreText!=null)
            scoreText.text = scoreKeeper.Score.ToString("00000000");
    
    }
    void HealthChangeHandler(int health){
        // Debug.Log("Health: " + health + " MaxHealth: "+ healthSlider.maxValue);
        if(healthSlider!=null){
            healthSlider.value = (float)health;
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
