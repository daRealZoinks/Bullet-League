using UnityEngine;

public class Goal : MonoBehaviour
{
    public CollisionCheck touch;
    public CollisionCheck dontTouch;

    void Update()
    {
        if (touch.colliding && !dontTouch.colliding)
        {
            //this is a goal
        }
    }
}
