using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [Header("Sway settings")] public float smooth = 5.0f;
    public float swayMultiplier = .35f;
    [Space] public PlayerMovement playerMovement;

    private void Update()
    {
        WeaponSway();
    }

    private void WeaponSway()
    {
        var rotationX = Quaternion.AngleAxis(-playerMovement.lookingDirection.y * swayMultiplier, Vector3.right);
        var rotationY = Quaternion.AngleAxis(playerMovement.lookingDirection.x * swayMultiplier, Vector3.up);

        var targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}