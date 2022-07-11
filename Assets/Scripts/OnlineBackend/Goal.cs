using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gameManager;

    public CollisionCheck touch;
    public CollisionCheck dontTouch;

    public Team TeamColor;

    public GameObject coloredPart;

    private void Update()
    {
        if (touch.colliding && !dontTouch.colliding)
        {
            if (TeamColor == Team.Blue)
            {
                gameManager.OrangeScored();
            }
            else
            {
                gameManager.BlueScored();
            }

            touch.colliding = false;
        }
    }
}