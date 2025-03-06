using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    int score=0;
    static ScoreKeeper instance;
public int Score
{
    get {return score;}
    set {
        if (score == value) return;
        score = value;
        if (OnScoreChange != null)
            OnScoreChange(score);
    }
}
public delegate void OnScoreChangeDelegate(int newVal);
public event OnScoreChangeDelegate OnScoreChange;

    public static ScoreKeeper GetInstance(){
        return instance;
    }
    void Awake()
    {
        ManageSingleTon();
    }

    void ManageSingleTon()
    {
        if(instance!= null){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // if (FindObjectsByType<ScoreKeeper>(FindObjectsSortMode.None).Length > 1)
        // {
        //     Destroy(gameObject);
        //     return false;
        // }
        // DontDestroyOnLoad(gameObject);
        // return true;
    }

    void Update()
    {
        
    }

    public int GetScore(){
        return Score;
    }
    public void IncreaseScore(int increment){
        Score += increment;
        Score = Mathf.Clamp(Score, 0, int.MaxValue);

        // Debug.Log("Score:" + score);
    }
    public void ResetScore(){
        Score =0;
    }
    

}
