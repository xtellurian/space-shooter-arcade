using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BgScroller : MonoBehaviour {
    public float ScrollSpeed;
    public float TileSize;
    private Vector3 _startPosition;

    // Use this for initialization
    void Start ()
    {
        _startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    var newPosition = Mathf.Repeat(Time.time * ScrollSpeed, TileSize);
	    transform.position = _startPosition + Vector3.forward*newPosition;
	}

    
}
