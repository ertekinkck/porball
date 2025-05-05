using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Singelton;
    public List<AudioClip> musics;
    public float volume = 0.5f;
    public float pitch = 1;

    private AudioSource source;

    [Header("Post Process")]
    private Volume postProcessingVolume;
    private Vignette vignette;
    private float vignetteMinIntensity = 0f;
    private float vignetteMaxIntensity = 0.2f;
    private float vignetteLerpSpeed = 5f;
    private float currentVignetteIntensity = 0f;

    private float[] spectrum = new float[64];

    void Awake()
    {
        if (Singelton == null) Singelton = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        StartMusic();
        TryFindVolumeInScene();
    }

    public void StartMusic()
    {
        AudioClip music = musics[Random.Range(0, musics.Count)];
        source.clip = music;
        source.Play();

        DOVirtual.Float(0, 0.3f, 3f, delegate (float n)
        {
            volume = n;
        });

        DOVirtual.DelayedCall(source.clip.length - 3, delegate
        {
            Tween outTween = DOVirtual.Float(0.3f, 0f, 3f, delegate (float n)
            {
                volume = n;
            });
            outTween.OnComplete(delegate
            {
                StartMusic();
            });
        });
    }

    void Update()
    {
        source.volume = volume;
        source.pitch = pitch;
        pitch = Mathf.Lerp(pitch, 1f, Time.deltaTime);

        if (source.isPlaying && vignette != null)
        {
            source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

            float max = 0f;
            for (int i = 0; i < 10; i++)
            {
                if (spectrum[i] > max)
                    max = spectrum[i];
            }

            float intensity = Mathf.Clamp01(max * 100f);
            float targetIntensity = Mathf.Lerp(vignetteMinIntensity, vignetteMaxIntensity, intensity);

            currentVignetteIntensity = Mathf.Lerp(currentVignetteIntensity, targetIntensity, Time.deltaTime * vignetteLerpSpeed);
            vignette.intensity.value = currentVignetteIntensity;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryFindVolumeInScene();
    }

    void TryFindVolumeInScene()
    {
        postProcessingVolume = FindAnyObjectByType<Volume>();

        if (postProcessingVolume != null && postProcessingVolume.profile.TryGet(out vignette))
        {
            currentVignetteIntensity = vignette.intensity.value;
        }
        else
        {
            vignette = null;
        }
    }
}
