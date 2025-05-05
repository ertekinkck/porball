using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioExtensions
{
    public static AudioSource PlayClip2D(this AudioClip clip, MonoBehaviour mono, float volume = 1, float pitch = 1)
    {
        if (clip == null) return null;
        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.Play();
        UnityEngine.Object.Destroy(go, audioSource.clip.length);
        return audioSource;
    }
    public static AudioSource PlayMusic2D(this AudioClip clip, MonoBehaviour mono, float volume = 1, float pitch = 1)
    {
        if (clip == null) return null;
        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
        return audioSource;
    }
    public static AudioSource PlayClip3D(this AudioClip clip, MonoBehaviour mono, Vector3 pos, float volume = 1, float pitch = 1)
    {
        if (clip == null) return null;
        GameObject go = new GameObject();
        go.transform.position = pos;
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.spatialBlend = 1;
        audioSource.maxDistance = 2;
        audioSource.Play();
        if (go != null)
            UnityEngine.Object.Destroy(go, audioSource.clip.length);
        return audioSource;
    }
}
