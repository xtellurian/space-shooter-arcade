using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{

    public Transform ShotSpawn;
    public GameObject Shot;
    public float FireRate;
    public float StartDelay;

    private AudioSource _audioSource;
	// Use this for initialization
	void Start () 
	{
	    _audioSource = GetComponent<AudioSource>();
	    InvokeRepeating("Fire", StartDelay, FireRate);
	}

    void Fire()
    {
        Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
        _audioSource.Play();
    }
}
