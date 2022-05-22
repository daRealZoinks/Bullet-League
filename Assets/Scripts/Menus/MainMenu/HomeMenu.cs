using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HomeMenu : MonoBehaviourPunCallbacks
{
    public Launcher launcher;

    public string[] quickGameMaps;
    public string[] eventMaps;

    TypedLobby lobby;

    public void QuickMatch()
    {
        lobby = new("Casual", LobbyType.Default);

        PhotonNetwork.JoinRandomOrCreateRoom(null, 0, MatchmakingMode.FillRoom, lobby, null, "Casual" + Random.Range(0, 200).ToString());
        launcher.OnlineButtonsActiveState(false);
    }

    public void Event()
    {
        lobby = new("Event", LobbyType.Default);

        PhotonNetwork.JoinRandomOrCreateRoom(null, 0, MatchmakingMode.FillRoom, lobby, null, "Event" + Random.Range(0, 200).ToString(), null, null);
        launcher.OnlineButtonsActiveState(false);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {

            Debug.Log(lobby);

            if (lobby.Name == "Casual")
            {
                PhotonNetwork.LoadLevel(quickGameMaps[Random.Range(0, quickGameMaps.Length)]);
            }

            if (lobby.Name == "Event")
            {
                PhotonNetwork.LoadLevel(eventMaps[Random.Range(0, eventMaps.Length)]);
            }
        }

        base.OnJoinedRoom();
    }

    public void Quit()
    {
        Application.Quit();
    }
}