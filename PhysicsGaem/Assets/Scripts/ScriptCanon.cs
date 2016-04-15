using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptCanon : MonoBehaviour {

    public GameObject cannonBall;
    public Image powerBar;
    public Image rotationIndicator;
    public Image ammoMask;

    public int numShots = 5;
    int shotsRemaining;

    [Header("Power Settings")]
    public float power = 30f;
    public float maxPower = 40f;
    public float minPower = 20f;

    [Header("Rotation Settings")]
    public float cannonRotation = 47f; //Initial cannon angle
    public float minCannonRotation = 30f; //Minimum cannon angle
    public float maxCannonRotation = 60f; //Maximum cannon angle
    public float rotationSpeed = 10f; //Cannon rotation per click

    float inversePowerRange;
    float degToRad = 0.0174533f;

    int numStars;
    int starsCollected = 0;

    // Use this for initialization
    void Start ()
    {
        inversePowerRange = 1 / (maxPower - minPower);
        powerBar.fillAmount = (power - minPower) * inversePowerRange;
        rotationIndicator.fillAmount = (cannonRotation / 360);
        shotsRemaining = numShots;
        ammoMask.fillAmount = (float)shotsRemaining / numShots;
        numStars = GameObject.FindGameObjectsWithTag("Star").Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.W))
        {
            if (power < maxPower)
            {
                power += 1;
                powerBar.fillAmount = (power - minPower) * inversePowerRange;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (power > minPower)
            {
                power -= 1;
                powerBar.fillAmount = (power - minPower) * inversePowerRange;
            }
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            if (cannonRotation < maxCannonRotation)
            {
                cannonRotation += rotationSpeed;
                transform.Rotate(Vector3.forward * rotationSpeed);
                rotationIndicator.fillAmount = (cannonRotation / 360);
            }
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (cannonRotation > minCannonRotation)
            {
                cannonRotation -= rotationSpeed;
                transform.Rotate(Vector3.back * rotationSpeed);
                rotationIndicator.fillAmount = (cannonRotation / 360);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (shotsRemaining > 0)
            {
                CannonBalls();
                shotsRemaining--;
                ammoMask.fillAmount = (float)shotsRemaining / numShots;
                
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
	}

    public void DestroyStar(GameObject star)
    {
        starsCollected++;
        Destroy(star);
        if (starsCollected >= numStars)
        {
            Debug.Log("Winning!");
        }
    }


    void CannonBalls()
    {
        GameObject cannonBallInstance = (GameObject)Instantiate(cannonBall, transform.position, Quaternion.identity);
        cannonBallInstance.GetComponent<Rigidbody2D>().velocity =
            new Vector2(power * Mathf.Cos(cannonRotation * degToRad), power * Mathf.Sin(cannonRotation * degToRad));
    }
}
