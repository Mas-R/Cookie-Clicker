using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Clips")]
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip clickSfxClip;

    [Header("Settings")]
    [SerializeField] private bool playBgmOnStart = true;
    [Range(0f, 1f)][SerializeField] private float bgmVolume = 0.8f;
    [Range(0f, 1f)][SerializeField] private float sfxVolume = 1f;

    private AudioSource _bgm;
    private AudioSource _sfx;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // BGM source
        _bgm = gameObject.AddComponent<AudioSource>();
        _bgm.playOnAwake = false;
        _bgm.loop = true;
        _bgm.spatialBlend = 0f;
        _bgm.volume = bgmVolume;

        // SFX source
        _sfx = gameObject.AddComponent<AudioSource>();
        _sfx.playOnAwake = false;
        _sfx.loop = false;
        _sfx.spatialBlend = 0f;
        _sfx.volume = sfxVolume;
    }

    private void Start()
    {
        if (playBgmOnStart) StartBGM();
    }

    public void StartBGM()
    {
        if (bgmClip == null) return;
        if (_bgm.isPlaying && _bgm.clip == bgmClip) return;
        _bgm.clip = bgmClip;
        _bgm.volume = bgmVolume;
        _bgm.Play();
    }

    public void StopBGM() => _bgm.Stop();

    public void SetBgmVolume(float v01)
    {
        bgmVolume = Mathf.Clamp01(v01);
        _bgm.volume = bgmVolume;
    }

    public void SetSfxVolume(float v01)
    {
        sfxVolume = Mathf.Clamp01(v01);
        _sfx.volume = sfxVolume;
    }

    public void PlayClick()
    {
        if (clickSfxClip == null) return;
        // PlayOneShot pakai volumeScale lokal agar tidak tergantung _sfx.volume saja
        _sfx.PlayOneShot(clickSfxClip, sfxVolume);
    }
}

