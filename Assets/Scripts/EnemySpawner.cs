using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;
    [SerializeField] bool isLooping =true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SortWaves();
        StartCoroutine(SpawnEnemyWaves());
        //  StartCoroutine(SpawnWaves());

    }

    // IEnumerator SpawnWaves()
    // {
    //     int maxWaves = GetMaxWaveNum();
    //     for (int i = 0; i < maxWaves; i++)
    //     {
    //         foreach (WaveConfigSO waveConfigSO in waveConfigs)
    //         {
    //             if (waveConfigSO.GetWaveNum() == i)
    //             {
    //                 Debug.Log("==>waveConfigSO: i="+i +" Max="+ maxWaves);
    //                 StartCoroutine(SpawnEnemyWaves(waveConfigSO));
    //             }
    //         }
    //         yield return new WaitForSeconds(timeBetweenWaves);
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    public WaveConfigSO GetCurrentWave(){
        return currentWave;
    }
    IEnumerator SpawnEnemyWaves(){
        do{
            foreach(WaveConfigSO waveConfigSO in waveConfigs){
                currentWave = waveConfigSO;
                for(int i=0;i<currentWave.GetEnemyCount();i++){
                    Instantiate(currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWayPoint().position,
                        Quaternion. identity,transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }while(isLooping);
    }

    // IEnumerator SpawnEnemyWaves(WaveConfigSO waveConfigSO){
    //      currentWave = waveConfigSO;
    //   //  Debug.Log("==>waveConfigSO: "+waveConfigSO.ToString());
    //     for(int i=0;i<currentWave.GetEnemyCount();i++){
    //         Pathfinder p = Instantiate(currentWave.GetEnemyPrefab(i),
    //             currentWave.GetStartingWayPoint().position,
    //             Quaternion.identity,transform).GetComponent<Pathfinder>();
    //             p.setWaveConfig(currentWave);
    //         yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
    //     }
    // }

    private int GetMaxWaveNum()
    {
        int maxWave = 0;
        foreach(WaveConfigSO wave in waveConfigs)
        {
            if(wave.GetWaveNum() > maxWave)
            {
                maxWave = wave.GetWaveNum();
            }
        }
        return maxWave;
    }
    private void SortWaves()
    {
        waveConfigs.Sort((a, b) => a.GetWaveNum().CompareTo(b.GetWaveNum()));
    }
}
