using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 0.5f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f,1f)] float damageVolume = 0.5f;

        public void PlayShootingClip(){
            PlayClip(shootingClip,shootingVolume);
        }

        public void PlayDamageClip(){
            PlayClip(damageClip,damageVolume);
        }
        void PlayClip(AudioClip clip, float volume){
            if(clip!=null){
                AudioSource.PlayClipAtPoint(clip,Camera.main.transform.position, volume);
            }
        }       



}
