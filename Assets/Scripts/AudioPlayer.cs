using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [SerializeField] private AudioClip soundClip;
    private AudioSource audioSource;

    void Start()
    {

    }

    public void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
}
