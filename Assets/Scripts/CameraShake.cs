using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude =0.2f;
    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake(){
        float shakeTimer = 0;
        while(shakeTimer<shakeDuration){
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeTimer+= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;

    }
}
