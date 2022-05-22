using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [Header("Sway settings")]
    public float smooth;
    public float swayMultiplier;

    [Space]
    public PlayerMovement playerMovement;

    private void Update()
    {
        WeaponSway();
    }

    void WeaponSway()
    {
        Quaternion rotationX = Quaternion.AngleAxis(-playerMovement.lookingDirection.y * swayMultiplier, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(playerMovement.lookingDirection.x * swayMultiplier, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}