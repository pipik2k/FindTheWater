using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip[] clips;
        public float volume = 1f;
        public bool loop = false;
    }

    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(musicSounds, sound => sound.name == name);
        if (s == null || s.clips.Length == 0) return;
        musicSource.clip = s.clips[Random.Range(0, s.clips.Length)];
        musicSource.loop = s.loop;
        musicSource.volume = s.volume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string name)
    {
        Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null || s.clips.Length == 0) return;
        sfxSource.loop = s.loop;
        sfxSource.volume = s.volume;
        sfxSource.PlayOneShot(s.clips[Random.Range(0, s.clips.Length)], s.volume);
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }
}
