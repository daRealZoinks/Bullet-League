using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicPopup : MonoBehaviour
{
    public Song[] songs;
    private Song currentSong;

    public AudioSource audioSource;
    public Image coverImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI artistsText;

    public new Animation animation;

    private void Update()
    {
        Debug.Log(audioSource.isPlaying);

        if (!audioSource.isPlaying)
        {
            currentSong = songs[Random.Range(0, songs.Length)];

            SongSetup();

            audioSource.Play();
        }
    }

    private void SongSetup()
    {
        audioSource.clip = currentSong.song;
        coverImage.sprite = currentSong.albumCover;
        titleText.text = currentSong.title;

        string artists = "";

        artists += currentSong.artists[0];

        if (currentSong.artists.Length > 1)
        {
            for (int i = 1; i < currentSong.artists.Length; i++)
            {
                artists += ", " + currentSong.artists[i];
            }
        }

        artistsText.text = artists;

        animation.Play();
    }
}