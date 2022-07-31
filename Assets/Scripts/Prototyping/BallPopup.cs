using UnityEngine;
using UnityEngine.UI;

public class BallPopup : MonoBehaviour
{
    public Image img;
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        img = GetComponentInParent<PlayerManager>().UI.ballPopup;
    }

    private void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        var pos = Camera.main.WorldToScreenPoint(target.position);

        if (Vector3.Dot(target.position - transform.forward, transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }

            if (pos.y < Screen.height / 2)
            {
                pos.y = maxY;
            }
            else
            {
                pos.y = minY;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos + offset;
    }
}