using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gameManager;

    [Space]

    public CollisionCheck touch;
    public CollisionCheck dontTouch;

    [Space]

    public Team teamColor;

    [Space]

    public GameObject backGoal;
    public GameObject goalField;

    [Space]

    public Material blue;
    public Material blueTransparent;
    public Material red;
    public Material redTransparent;

    private void Update()
    {
        if (!touch.colliding || dontTouch.colliding) return;

        switch (teamColor)
        {
            default:
            case Team.Blue:
                gameManager.OrangeScored();
                break;
            case Team.Orange:
                gameManager.BlueScored();
                break;
        }

        touch.colliding = false;
        dontTouch.colliding = true;
    }
}