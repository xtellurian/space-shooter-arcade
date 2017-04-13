using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float Speed;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.velocity = transform.forward * Speed;

    }
}
