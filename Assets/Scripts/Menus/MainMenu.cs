using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public Button[] onlineButtons;

    [Space] public TMP_InputField nameInput;
    public TextMeshProUGUI gameState;

    public void ChangeName(string text)
    {
        PlayerPrefs.SetString("PlayerName", text);
        PhotonNetwork.NickName = text;
    }

    private void OnlineButtonsActiveState(bool state)
    {
        foreach (var button in onlineButtons)
        {
            button.interactable = state;
        }
    }

    private void Awake()
    {
        nameInput.text = PlayerPrefs.GetString("PlayerName");
        gameState.text = "Offline";

        OnlineButtonsActiveState(false);

        Connect();
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            return;
        }

        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
        gameState.text = "Connecting";
    }

    public override void OnConnectedToMaster()
    {
        OnlineButtonsActiveState(true);
        gameState.text = "Online";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}