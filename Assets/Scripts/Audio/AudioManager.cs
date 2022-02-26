using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��Ƶ��������
/// </summary>
public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;

    const float MIN_PITCH = 0.9f;
    const float MAX_PITCH = 1.1f;

    /// <summary>
    /// �ʺϲ���UI��Ч
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
    /// �ʺϲ����ظ���Ч
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlayerSFX(audioData);
    }
    /// <summary>
    /// ��Ƶ��������
    /// </summary>
    /// <param name="audioData"></param>
    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }
}

/// <summary>
/// ��Ƶ-����-��Ƶ��
/// </summary>
[System.Serializable] public class AudioData
{
    public AudioClip audioClip;
    public float volume;
}
