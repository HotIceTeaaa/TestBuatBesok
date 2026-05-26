using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource ambientAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    [SerializeField] private AudioClip bgMusic;
    [SerializeField] private AudioClip ambient;

    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip dashReady;

    public static audioManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void playBGMusic()
    {
        musicAudioSource.Play();
        //musicAudioSource.
    } 

    public void playAmbient()
    {
        ambientAudioSource.Play();
    } 


}
