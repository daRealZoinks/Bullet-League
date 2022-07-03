using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviourPunCallbacks
{
    public Transform content;
    public JoinGameButton button;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            JoinGameButton joinGameButton = Instantiate(button, content);
            if (joinGameButton != null)
            {
                button.SetRoomInfo(info);
            }
        }
    }
}