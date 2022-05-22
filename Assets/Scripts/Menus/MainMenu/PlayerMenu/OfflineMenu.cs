using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfflineMenu : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI gameState;

    public Button[] offlineButtons;

    //private void Awake()
    //{
    //    for (int i = 0; i < offlineButtons.Length; i++)
    //    {
    //        offlineButtons[i].enabled = false;
    //    }
    //    if (PhotonNetwork.IsConnected)
    //    {
    //        PhotonNetwork.Disconnect();
    //    }
    //    gameState.text = "Disconnecting";
    //}

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    for (int i = 0; i < offlineButtons.Length; i++)
    //    {
    //        offlineButtons[i].enabled = true;
    //    }
    //    PhotonNetwork.OfflineMode = true;
    //    gameState.text = "Offline Mode";
    //    base.OnDisconnected(cause);
    //}

    public void Freeplay()
    {
        PhotonNetwork.OfflineMode = true;
        SceneManager.LoadScene("TestScene");
    }

    public void Splitscreen()
    {

    }

    public void TrainingWithAI()
    {

    }
}