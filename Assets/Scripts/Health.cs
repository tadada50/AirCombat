using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool isPlayer;

    int initialHealth;
    float criticalHealth = 0.25f;
    CameraShake cameraShake;

    [SerializeField] SpriteRenderer spriteRenderer;
    float blinkInterval = 0.01f;
    bool isBlinking = false;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    Player player;



public int CurrentHealth
{
    get {return health;}
    set {
        if (health == value) return;
        health = value;
        if (OnHealthChange != null)
            OnHealthChange(health);
    }
}
public delegate void OnHealthChangeDelegate(int newVal);
public event OnHealthChangeDelegate OnHealthChange;




    void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
        initialHealth= CurrentHealth;
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        player = FindFirstObjectByType<Player>();
    }
    void Start()
    {
        
        cameraShake = Camera.main.GetComponent<CameraShake>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(isPlayer){
           BlinkSprite();
        }
        
    }
    public int GetHealth(){
        return CurrentHealth;        
    }
    public int GetInitialHealth(){
        return initialHealth;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Hit");
        // Debug.Log("Other object: " + other.gameObject.name);
        // Debug.Log("Collision location: " + other.transform.position);
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        // 
        // Debug.Log("Self: " + gameObject.name);
        if(damageDealer!=null)
        {
            // Debug.Log("DamageDealer: " + damageDealer.name);
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            damageDealer.Hit();
            ShakeCamera();
        }
    }

    private void ShakeCamera()
    {
        if (isPlayer && cameraShake != null)
        {
            cameraShake.Play();
        }
    }

    void TakeDamage(int damage){
        audioPlayer.PlayDamageClip();
            CurrentHealth -= damage;
            if(CurrentHealth<=0){
                Die();
            }
    }

    void Die(){
        if(!isPlayer){
            scoreKeeper.IncreaseScore(score);
        }
        Destroy(gameObject);
    }

    void PlayHitEffect(){
        if(hitEffect != null){
            ParticleSystem instance = Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

        void BlinkSprite()
    {
        if (criticalHealth > (float)CurrentHealth/(float)initialHealth && !isBlinking)
        {
            isBlinking = true;
            InvokeRepeating("ToggleColor", 0f, blinkInterval);
        }
        else if (CurrentHealth >= 900 && isBlinking)
        {
            isBlinking = false;
            CancelInvoke("ToggleColor");
            if (spriteRenderer != null)
                spriteRenderer.color = Color.white; // Reset to default color
        }
    }

    float colorLerpTime = 0f;
    void ToggleColor()
    {
        if (spriteRenderer != null)
        {
            colorLerpTime = (colorLerpTime + Time.deltaTime) % 1f;
            Color lerpedColor = Color.Lerp(Color.white, Color.red, Mathf.PingPong(colorLerpTime * 2f, 1f));
            spriteRenderer.color = lerpedColor;
        }
    }

    void ToggleSprite()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}
