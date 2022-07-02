using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HostGame : MonoBehaviourPunCallbacks
{
    private int roomSize = 2;

    private string roomName;

    private RoomOptions roomOptions = new();

    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        roomSize = (numberOfPlayers + 1) * 2;
        roomOptions.MaxPlayers = (byte)roomSize;
    }

    public void SetRoomName(string name)
    {
        roomName = name;
        GameObject gj;
    }

    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);

        if (roomName != "" && roomName != null && PhotonNetwork.NickName != null)
        {
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed");
    }
}