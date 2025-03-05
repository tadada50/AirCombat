using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool isPlayer;

    int initialHealth;
    float criticalHealth = 0.25f;
    CameraShake cameraShake;

    [SerializeField] SpriteRenderer spriteRenderer;
    float blinkInterval = 0.01f;
    bool isBlinking = false;
    AudioPlayer audioPlayer;


    void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
    }
    void Start()
    {
        initialHealth= health;
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
            health -= damage;
            if(health<=0){
                Destroy(gameObject);
            }
    }


    void PlayHitEffect(){
        if(hitEffect != null){
            ParticleSystem instance = Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

        void BlinkSprite()
    {
        if (criticalHealth > (float)health/(float)initialHealth && !isBlinking)
        {
            isBlinking = true;
            InvokeRepeating("ToggleColor", 0f, blinkInterval);
        }
        else if (health >= 900 && isBlinking)
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
        float frac; 
        if (spriteRenderer != null)
        {
            frac = (Math.Abs(Mathf.Sin(Time.time + colorLerpTime)));
            // frac = (Mathf.Sin(Time.time + colorLerpTime) * 1f);
            colorLerpTime = (colorLerpTime + Time.deltaTime) % 1f;
            // frac = Mathf.PingPong(colorLerpTime * 2f, 1f);
            // Color lerpedColor = Color.Lerp(Color.white, Color.red, frac);
            // Color lerpedColor = Color.Lerp(Color.red, Color.white, frac);
            Color lerpedColor = Color.Lerp(Color.white, Color.red, Mathf.PingPong(colorLerpTime * 2f, 1f));
            // Color lerpedColor = Color.Lerp(Color.white, Color.red, frac);
            spriteRenderer.color = lerpedColor;
            //  Debug.Log($"LerpedColor: {lerpedColor}, Fraction: {frac}");
        }
    }

    void ToggleSprite()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}
