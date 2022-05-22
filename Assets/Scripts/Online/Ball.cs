using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.currentBall = gameObject;
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
