using UnityEngine;
using System.Collections;

public class ScriptCanon : MonoBehaviour {

    public GameObject cannonBall;
    public float power = 30f;
    public float maxPower = 40f;
    public float minPower = 20f;
    public float cannonRotation = 49f;
    public float minCannonRotation = 30f;
    public float maxCannonRotation = 60f;

    float degToRad = 0.0174533f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.W))
        {
            if (power < maxPower)
            {
                power += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (power > minPower)
            {
                power -= 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (cannonRotation < maxCannonRotation)
            {
                cannonRotation += 1;
                transform.Rotate(Vector3.forward);
            }
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (cannonRotation > minCannonRotation)
            {
                cannonRotation -= 1;
                transform.Rotate(Vector3.back);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CannonBalls();
        }
	}

    void CannonBalls()
    {
        GameObject cannonBallInstance = (GameObject)Instantiate(cannonBall, transform.position, Quaternion.identity);
        cannonBallInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(power * Mathf.Cos(cannonRotation * degToRad), power * Mathf.Sin(cannonRotation * degToRad));
    }
}
