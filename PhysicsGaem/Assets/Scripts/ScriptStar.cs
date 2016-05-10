using UnityEngine;
using System.Collections;

public class ScriptStar : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "GameController" || other.transform.tag == "Player")
        {
            GameObject.Find("Cannon").GetComponent<ScriptCanon>().DestroyStar(gameObject);
        }
    }
}
