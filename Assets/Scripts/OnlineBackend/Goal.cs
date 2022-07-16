using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gameManager;

    public CollisionCheck touch;
    public CollisionCheck dontTouch;

    public Team TeamColor;

    public GameObject backGoal;
    public GameObject goalField;

    public Material blue;
    public Material blueTransparent;
    public Material red;
    public Material redTransparent;

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