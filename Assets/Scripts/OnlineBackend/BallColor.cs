using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : MonoBehaviour
{
    private Material _material;
    [SerializeField] private Color blueColor;
    [SerializeField] private Color middleColor = Color.white;
    [SerializeField] private Color orangeColor;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
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