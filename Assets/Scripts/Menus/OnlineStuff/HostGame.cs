using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HostGame : MonoBehaviour
{
    private int roomSize = 2;
    private string roomName;

    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        roomSize = (numberOfPlayers + 1) * 2;
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new()
        {
            MaxPlayers = (byte)roomSize
        };

        if (PhotonNetwork.IsConnected)
        {
            if (roomName != "" && roomName != null && PhotonNetwork.NickName != null)
            {
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            }
        }
    }
}