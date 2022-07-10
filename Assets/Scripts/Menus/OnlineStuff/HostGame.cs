using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HostGame : MonoBehaviour
{
    private int roomSize = 2;

    private string roomName;

    private readonly RoomOptions roomOptions = new();

    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        roomSize = (numberOfPlayers + 1) * 2;
        roomOptions.MaxPlayers = (byte)roomSize;
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (roomName != "" && roomName != null && PhotonNetwork.NickName != null)
            {
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            }
        }
    }
}