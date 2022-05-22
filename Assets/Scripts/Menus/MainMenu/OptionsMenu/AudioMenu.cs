using UnityEngine;
using UnityEngine.Audio;

public class AudioMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void Volume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }
}