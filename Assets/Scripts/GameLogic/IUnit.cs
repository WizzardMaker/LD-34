using UnityEngine;
using System.Collections;

public class IUnit : MonoBehaviour {

	public GameObject crashPrefab, explosionPrefab;

	public float maxLife = 10f;
	public float life;

	protected bool dead = false;

	protected float timeUntilRespawn = 5f, timeLeftRespawn = 5f;
	// Use this for initialization
	protected void Start() {
		life = maxLife;
		timeLeftRespawn = timeUntilRespawn;
	}

	protected void Update() {
		LifeUpdate();

	}

	protected void Revive() {
		transform.position = new Vector3(Random.Range(50, 144), 0, Random.Range(43, 139));
		life = maxLife;
		dead = false;
		timeLeftRespawn = timeUntilRespawn;
		foreach (Renderer go in gameObject.GetComponentsInChildren<Renderer>()) {
			go.enabled = true;
		}
	}

	// Update is called once per frame
	protected void LifeUpdate() {
		if(life <= 0) {
			if (tag == "Player") {
				//Debug.LogWarning("GameOver!");
				//Instantiate(explosionPrefab, transform.position, Quaternion.identity);
				if (timeLeftRespawn == timeUntilRespawn) {
					Debug.Log("Killed!");
					dead = true;
					Instantiate(explosionPrefab, transform.position, Quaternion.identity);
					foreach(Renderer go in gameObject.GetComponentsInChildren<Renderer>()) {
						go.enabled = false;
					}
				}
				transform.position = transform.position;
				timeLeftRespawn -= Time.deltaTime;
				if (timeLeftRespawn <= 0)
					Revive();
			} else {
				if (timeLeftRespawn == timeUntilRespawn) {
					Debug.Log("Killed!");
					Instantiate(explosionPrefab, transform.position, Quaternion.identity);
				}
				//Destroy(gameObject);
				transform.position = -Vector3.one * Random.Range(-200f,-400f);
				timeLeftRespawn -= Time.deltaTime;
				if (timeLeftRespawn <= 0)
					Revive();
			}

		}


	}
}
