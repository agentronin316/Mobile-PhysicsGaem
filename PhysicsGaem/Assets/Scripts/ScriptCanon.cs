using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptCanon : MonoBehaviour {

    public GameObject cannonBall;
    public Image powerBar;
    public Image rotationIndicator;
    public Image ammoMask;
    public Text progressDisplay;
    public CameraFollow followScript;
    public Transform shotSpawn;

    ScriptControls gameControls;

    public int numShots = 5;
    int shotsRemaining;

    [Header("Power Settings")]
    public float power = 30f;
    public float maxPower = 40f;
    public float minPower = 20f;

    [Header("Rotation Settings")]
    public float cannonRotation = 45f; //Initial cannon angle
    public float minCannonRotation = 30f; //Minimum cannon angle
    public float maxCannonRotation = 60f; //Maximum cannon angle
    public float rotationSpeed = 10f; //Cannon rotation per click

    float inversePowerRange;
    float degToRad = 0.0174533f;

    int numStars;
    int starsCollected = 0;
    int starRating = 0;

    // Use this for initialization
    void Start ()
    {
        gameControls = GameObject.Find("Canvas").GetComponent<ScriptControls>();
        if (shotSpawn == null)
        {
            shotSpawn = transform.GetChild(0);
        }
        inversePowerRange = 1 / (maxPower - minPower);
        powerBar.fillAmount = (power - minPower) * inversePowerRange;
        rotationIndicator.fillAmount = (cannonRotation / 360);
        shotsRemaining = numShots;
        ammoMask.fillAmount = (float)shotsRemaining / numShots;
        numStars = GameObject.FindGameObjectsWithTag("Star").Length;
        UpdateLevelProgress();
        followScript = Camera.main.GetComponent<CameraFollow>();
    }
	
    void OnApplicationPause()
    {
        ScriptLevelRatings.SaveData();
    }

	// Update is called once per frame
	//void Update ()
 //   {
	//    if(Input.GetKeyDown(KeyCode.W))
 //       {
 //           IncreasePower();
 //       }
 //       else if (Input.GetKeyDown(KeyCode.S))
 //       {
 //           DecreasePower();
 //       }


 //       if (Input.GetKeyDown(KeyCode.A))
 //       {
 //           IncreaseAngle();
 //       }
 //       else if(Input.GetKeyDown(KeyCode.D))
 //       {
 //           DecreaseAngle();
 //       }
 //       if(Input.GetKeyDown(KeyCode.Space))
 //       {
 //           FireCannon();
 //       }
	//}

    void UpdateLevelProgress()
    {
        progressDisplay.text = starsCollected + " / " + numStars;
        if(starsCollected / (float)numStars > .85f)
        {
            starRating = 2;
        }
        else if(starsCollected / (float)numStars > .75f)
        {
            starRating = 1;
        }
    }

    public void FireCannon()
    {
        if (followScript.player != null)
        {
            Destroy(followScript.player.gameObject);
            followScript.trackingShot = false;
        }
        if (shotsRemaining > 0)
        {
            CannonBalls();
            shotsRemaining--;
            ammoMask.fillAmount = (float)shotsRemaining / numShots;
        }
        else
        {
            ShowResults();
        }
    }

   
    public void IncreaseAngle()
    {
        if (cannonRotation < maxCannonRotation)
        {
            cannonRotation += rotationSpeed;
            transform.Rotate(Vector3.forward * rotationSpeed);
            rotationIndicator.fillAmount = (cannonRotation / 360);
        }
    }

    public void DecreaseAngle()
    {
        if (cannonRotation > minCannonRotation)
        {
            cannonRotation -= rotationSpeed;
            transform.Rotate(Vector3.back * rotationSpeed);
            rotationIndicator.fillAmount = (cannonRotation / 360);
        }
    }

    public void IncreasePower()
    {
        if (power < maxPower)
        {
            power += 1;
            powerBar.fillAmount = (power - minPower) * inversePowerRange;
        }
    }

    public void DecreasePower()
    {
        if (power > minPower)
        {
            power -= 1;
            powerBar.fillAmount = (power - minPower) * inversePowerRange;
        }
    }

    public void DestroyStar(GameObject star)
    {
        starsCollected++;
        Destroy(star);
        UpdateLevelProgress();
        if (starsCollected >= numStars)
        {
            starRating = 3;
            ShowResults();
        }
    }

    void ShowResults()
    {
        GameObject.Find("Canvas").GetComponent<ScriptControls>().DisplayResults(starRating);
        
    }

    void CannonBalls()
    {
        GameObject cannonBallInstance = (GameObject)Instantiate(cannonBall, shotSpawn.position, Quaternion.identity);
        cannonBallInstance.GetComponent<Rigidbody2D>().velocity =
            new Vector2(power * Mathf.Cos(cannonRotation * degToRad), power * Mathf.Sin(cannonRotation * degToRad));
        followScript.shotFired = true;
    }
}
