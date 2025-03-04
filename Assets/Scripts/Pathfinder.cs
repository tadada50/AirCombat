using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig = null;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();

        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
        // StartCoroutine(InitiateAfterSettingComplete());
    }

    IEnumerator InitiateAfterSettingComplete(){
        yield return new WaitUntil(() => waveConfig != null);
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }
    void Update()
    {
        FollowPath();   
    }

    // void Update()
    // {
    //     StartCoroutine(FollowPath());
    // }
    public void setWaveConfig(WaveConfigSO waveConfigSO){
        waveConfig = waveConfigSO;
    }
    // IEnumerator FollowPath(){
    //     yield return new WaitUntil(() => waypoints != null);
    //     if(waypointIndex < waypoints.Count){
    //         Vector3 targetPosition = waypoints[waypointIndex].position;
    //         float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
    //         transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
    //         if(transform.position == targetPosition){
    //             waypointIndex++;
    //         }
    //     }else{
    //         Destroy(gameObject);
    //     }
    // }

    
    void FollowPath(){
        if(waypointIndex < waypoints.Count){
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition){
                waypointIndex++;
            }
        }else{
            Destroy(gameObject);
        }
    }
}
