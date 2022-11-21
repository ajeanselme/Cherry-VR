using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    public Sound[] sounds;

    private AudioMixer audioMixer;

    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Load AudioMixer
            audioMixer = Resources.Load<AudioMixer>("Audio/NewAudioMixer");
            AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master");

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clips[0];
                s.source.volume = (s.volume == 0) ? 1.0f : s.volume;
                s.source.pitch = (s.pitch == 0) ? 1.0f : s.pitch;
                s.source.spatialBlend = s.spatialBlend;
                s.source.minDistance = s.minDist3D;
                s.source.maxDistance = s.maxDist3D;
                s.source.mute = s.mute;
                s.source.loop = s.loop;
                s.source.playOnAwake = s.playOnAwake;
                s.source.outputAudioMixerGroup = audioMixGroup[0];
            }

            // | Listen To
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //PlaySound("Main");
    }

    //How to use : SoundManager.Instance.PlaySound(name);
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !\nCheck name spelling");
            return;
        }
        if (s.clips.Length > 1)
        {
            int clipNumber = UnityEngine.Random.Range(0, s.clips.Length);
            s.source.clip = s.clips[clipNumber];
        }
        s.source.Play();
    }

    public void StopAllSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void StopAllSoundWithFade()
    {
        foreach (Sound s in sounds)
        {
            StartCoroutine(FadeSound(s));
        }
    }
    public void StopSoundWithFade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(FadeSound(s));
    }

    private IEnumerator FadeSound(Sound s)
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeDuration;
            s.source.volume = Mathf.Lerp(1, 0, ratio);
            yield return null;
        }

        s.source.Stop();
        s.source.volume = 1;
    }

    public void ChangeMute(bool mute)
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = mute;
        }
    }

    public void SetMasterVolume(float sliderValue)
    {
        if (sliderValue <= 0.1)
        {
            audioMixer.SetFloat("MasterVolume", 0.01f);
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        }

    }

    public bool IsSoundPlaying(string sound)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == sound)
            {
                return s.source.isPlaying;
            }
        }

        return false;
    }
}

[System.Serializable]
public class Sound
{
    public string name;

    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Range(-3f, 3f)]
    public float pitch = 1.0f;
    [Range(0f, 1f)]
    public float spatialBlend = 0f;
    public float minDist3D = 1f;
    public float maxDist3D = 500f;
    public bool mute = false;
    public bool loop = false;
    public bool playOnAwake = true;

    public AudioClip[] clips;

    [HideInInspector]
    public AudioSource source;
}