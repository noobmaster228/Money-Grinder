using System.Collections.Generic;
using UnityEngine;

public class BonusSoundPlayer : MonoBehaviour
{
    [System.Serializable]   
    public class BonusSound
    {
        public string tag;
        public AudioClip clip;
    }
    [SerializeField] List<BonusSound> bonusSounds;
    [SerializeField] Dictionary<string, AudioClip> soundMap;
    [SerializeField] AudioSource audioSource;
    void Awake()
    {
        soundMap = new Dictionary<string, AudioClip>();
        foreach (var sound in bonusSounds)
        {
            if (!soundMap.ContainsKey(sound.tag))
                soundMap.Add(sound.tag, sound.clip);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlayBonusSound(string tag)
    {
        if (soundMap.TryGetValue(tag, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
