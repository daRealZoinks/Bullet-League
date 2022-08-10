using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    public SoundCue ballTouchSoundCue;

    [Space]

    private Material _material;
    [SerializeField] private Color blueColor;
    [SerializeField] private Color middleColor = Color.white;
    [SerializeField] private Color orangeColor;

    private void OnCollisionEnter(Collision collision)
    {
        ballTouchSoundCue.PlayRandomSoundAtPosition(transform.position, collision.relativeVelocity.magnitude);
    }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.z > 0)
        {
            _material.color = Color.Lerp(middleColor, blueColor, transform.position.z / 100);
        }
        else
        {
            _material.color = Color.Lerp(middleColor, orangeColor, -transform.position.z / 100);
        }
    }
}