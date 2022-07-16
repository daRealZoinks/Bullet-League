using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviourPunCallbacks
{
    public Transform content;
    public JoinGameButton button;

    private readonly List<JoinGameButton> buttonList = new();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = buttonList.FindIndex(x => x.roomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(buttonList[index].gameObject);
                    buttonList.RemoveAt(index);
                }
            }
            else
            {
                JoinGameButton joinGameButton = Instantiate(button, content);
                if (joinGameButton != null)
                {
                    joinGameButton.SetRoomInfo(info);
                    buttonList.Add(joinGameButton);
                }
            }
        }
    }
}