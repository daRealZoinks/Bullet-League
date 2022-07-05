using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode)
        {
            if (collision.gameObject.CompareTag("BlueGoal"))
            {
                gameManager.OrangeScored();
            }

            if (collision.gameObject.CompareTag("OrangeGoal"))
            {
                gameManager.BlueScored();
            }
        }
    }
}
