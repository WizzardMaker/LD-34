using UnityEngine;
using System.Collections;

public class DeathAfterTime : MonoBehaviour {

	public float timeUntilDeath = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeUntilDeath -= Time.deltaTime;

		if (timeUntilDeath <= 0)
			Destroy(gameObject);
	}
}
