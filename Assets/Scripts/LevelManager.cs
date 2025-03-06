using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 3f;
    public void LoadGame(){
        // SceneManager.LoadScene(1);
        ScoreKeeper.GetInstance().Score = 0;
        SceneManager.LoadScene("Level1");
    }
    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGameOver(){
        StartCoroutine(WaitAndLoad("GameOver",sceneLoadDelay));
        // SceneManager.LoadScene("GameOver");
    }
    public void QuitGame(){
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
