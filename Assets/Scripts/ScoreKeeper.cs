using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score=0;

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
