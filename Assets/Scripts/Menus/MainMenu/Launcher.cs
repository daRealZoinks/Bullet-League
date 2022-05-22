using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public Button[] onlineButtons;

    public TMP_InputField nameInput;
    public TextMeshProUGUI gameState;

    public void ChangeName(string text)
    {
        PlayerPrefs.SetString("PlayerName", text);
        PhotonNetwork.NickName = text;
    }

    private void Awake()
    {
        nameInput.text = PlayerPrefs.GetString("PlayerName");

        OnlineButtonsActiveState(false);

        PhotonNetwork.AutomaticallySyncScene = false;
        StartCoroutine(Connect());

        gameState.text = "Connecting";
    }

    public IEnumerator Connect()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
        if (!PhotonNetwork.IsConnected)
        {
            yield return new WaitForSeconds(5);
            StartCoroutine(Connect());
        }
    }

    public override void OnConnected()
    {
        OnlineButtonsActiveState(true);

        gameState.text = "Connected";

        PhotonNetwork.AutomaticallySyncScene = true;

        base.OnConnected();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        gameState.text = "JoinFailed";

        base.OnJoinRandomFailed(returnCode, message);
    }

    public void OnlineButtonsActiveState(bool state)
    {
        for (int i = 0; i < onlineButtons.Length; i++)
        {
            onlineButtons[i].interactable = state;
        }
    }
}