using UnityEngine;
using System.Collections;

public class ScriptClearingVolume : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Star":
                GameObject.Find("Cannon").GetComponent<ScriptCanon>().DestroyStar(other.gameObject);
                break;
            case "Player":
                other.gameObject.GetComponent<Rigidbody2D>().Sleep();
                break;
            case "GameController":
                //Do nothing
                break;
            default:
                Destroy(other.gameObject);
                break;
        }
    }
}
