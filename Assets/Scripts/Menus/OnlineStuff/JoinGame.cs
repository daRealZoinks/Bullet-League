using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{
    //list for buttons

    private List<JoinGameButton> joinGameButtons;

    private void Start()
    {
        RefreshRoomList();
    }

    private void RefreshRoomList()
    {
        joinGameButtons.Clear();

        for (int i = 0; i < PhotonNetwork.CountOfRooms; i++)
        {
            //joinGameButtons.Add();
        }
        //make new buttons representing the rooms
    }
}
