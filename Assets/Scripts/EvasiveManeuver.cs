using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
    public float Dodge;
    public float Tilt;
    public Vector2 StartWait;
    public Vector2 ManeuvreTime;
    public Vector2 ManeuvreWait;
    public float Smoothing;
    private Rigidbody _rb;
    private float _currentSpeed;
    private float _targetManeuvre;

    public Boundary boundary;
    // Use this for initialization
	void Start ()
	{
	    _rb = GetComponent<Rigidbody>();
	    _currentSpeed = _rb.velocity.z;
	    StartCoroutine(Evade());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(StartWait.x, StartWait.y));
        while (true)
        {
            _targetManeuvre = Random.Range(1, Dodge) * - Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(ManeuvreTime.x, ManeuvreTime.y));
            _targetManeuvre = 0;
            yield return new WaitForSeconds(Random.Range(ManeuvreTime.x, ManeuvreTime.y));
        }
    }

    void FixedUpdate()
    {
        var newManeuvre = Mathf.MoveTowards(_rb.velocity.x, _targetManeuvre, Time.deltaTime*Smoothing);
        _rb.velocity = new Vector3(newManeuvre, 0.0f, _currentSpeed);
        _rb.position = new Vector3(
           Mathf.Clamp(_rb.position.x, boundary.XMin, boundary.XMax),
           0.0f,
           Mathf.Clamp(_rb.position.z, boundary.ZMin, boundary.ZMax)
           );

        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x*-Tilt);
    }
}
