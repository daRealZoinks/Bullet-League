using UnityEngine;

[CreateAssetMenu(fileName = "Song")]
public class Song : ScriptableObject
{
    public AudioClip song;
    public Sprite albumCover;
    public string title;
    public string[] artists;
}