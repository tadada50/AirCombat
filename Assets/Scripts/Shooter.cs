using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed=10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.5f;
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float enemyFireVariance = 0.5f;
    [SerializeField] float minFiringRate = 0.1f;

    [HideInInspector] public bool isFiring = true;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(useAI){
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null){
            firingCoroutine = StartCoroutine(FireContinuously());
        }else if(!isFiring && firingCoroutine!= null){
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
        }
        
    }

    IEnumerator FireContinuously(){
        while(true){
            GameObject projectile = Instantiate(projectilePrefab,
                        transform.position,
                        Quaternion. identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if(rb!=null){
                rb.linearVelocity = transform.up * projectileSpeed;
            }
                        Destroy(projectile,projectileLifetime);
            if(useAI){
                float timeToNextProjectile = Random.Range(baseFiringRate - enemyFireVariance,baseFiringRate+enemyFireVariance);
                timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);
                baseFiringRate = timeToNextProjectile;
            }else{
                audioPlayer.PlayShootingClip();
            }
            yield return new WaitForSeconds(baseFiringRate);
            
        }
    }
}
