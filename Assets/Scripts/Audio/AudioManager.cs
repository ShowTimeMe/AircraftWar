using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 音频管理器类
/// </summary>
public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;

    const float MIN_PITCH = 0.9f;
    const float MAX_PITCH = 1.1f;

    /// <summary>
    /// 适合播放UI音效
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    public void PlayerSFX(AudioData audioData)
    {
        //sFXPlayer.clip = audioClip;
        //sFXPlayer.volume = volume;
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volume);
    }

    /// <summary>
    /// 适合播放重复音效
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlayerSFX(audioData);
    }
    /// <summary>
    /// 音频播放数组
    /// </summary>
    /// <param name="audioData"></param>
    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }
}

/// <summary>
/// 音频-声音-音频类
/// </summary>
[System.Serializable] public class AudioData
{
    public AudioClip audioClip;
    public float volume;
}
