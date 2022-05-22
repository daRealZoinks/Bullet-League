using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class OnlineMenu : MonoBehaviour
{
    public Launcher launcher;

    public void Ranked()
    {
        launcher.OnlineButtonsActiveState(false);
    }

    public void Casual()
    {
        TypedLobby normalMode = TypedLobby.Default;

        PhotonNetwork.JoinRandomOrCreateRoom(null, 6, MatchmakingMode.FillRoom, normalMode, null, "Casual" + Random.Range(0, 200).ToString(), null, null);
        launcher.OnlineButtonsActiveState(false);
    }

    public void Public()
    {
        launcher.OnlineButtonsActiveState(false);
    }

    public void Private()
    {
        launcher.OnlineButtonsActiveState(false);
    }
}