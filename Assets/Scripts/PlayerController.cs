using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float XMin, XMax, ZMin, ZMax;
}

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float Tilt;
    public Boundary boundary;
    public SimpleTouchPad TouchPad;
    public SimpleTouchAreaButton AreaButton;
    public GameObject Shot;
    public Transform ShotSpawnTransform;
    public float FireRate = 0.5F;
    private float _nextFire = 0.0F;
    private Rigidbody _rb;
    private AudioSource _audio;
    private Quaternion _calibrationQuaternion;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
        // CalibrateAccellerometer();
    }

    void Update()
    {

        if (Time.time > _nextFire && AreaButton.CanFire() )
        {
            _nextFire = Time.time + FireRate;
            Instantiate(Shot, ShotSpawnTransform.position, ShotSpawnTransform.rotation);

            _audio.Play();
        }


    }
    void FixedUpdate ()
	{
        //var moveHorizontal = Input.GetAxis("Horizontal");
        //var moveVertical = Input.GetAxis("Vertical");

        var direction = TouchPad.GetDirection();
        var movement = new Vector3(direction.x, 0.0f, direction.y);
        _rb.velocity = movement * Speed;

        _rb.position = new Vector3(
            Mathf.Clamp(_rb.position.x, boundary.XMin,boundary.XMax),
            0.0f,
            Mathf.Clamp(_rb.position.z,boundary.ZMin,boundary.ZMax)
            );

        _rb.rotation = Quaternion.Euler(0.0f,0.0f, _rb.velocity.x * - Tilt);
	}

    void CalibrateAccellerometer()
    {
        var accellerationSnapshot = Input.acceleration;
        var rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accellerationSnapshot);
        _calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        Debug.Log(_calibrationQuaternion);
    }

    Vector3 FixAccelleration(Vector3 accelleration)
    {
        var fixedAccelleration = _calibrationQuaternion*accelleration;
        return fixedAccelleration;
    }


}
