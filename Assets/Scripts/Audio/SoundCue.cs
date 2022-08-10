using UnityEngine;

[CreateAssetMenu(fileName = "SoundCue", menuName = "Audio/SoundCue", order = 6)]
/// <summary>
/// A sound cue is a collection of audio clips that can be played at a certain point in time.
/// </summary>
public class SoundCue : ScriptableObject
{
    /// <summary>
    /// The audio clips that are part of this sound cue.
    /// </summary>
    public AudioClip[] clips;

    /// <summary>
    /// Plays a random clip from the collection at the given position.
    /// </summary>
    /// <param name="position"> The position at which the sound should be played. </param>
    /// <param name="volume"> The volume at which the sound should be played. </param>
    public void PlayRandomSoundAtPosition(Vector3 position, float volume = 1.0f)
    {
        var clip = clips[Random.Range(0, clips.Length)];
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}
