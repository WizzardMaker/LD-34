using UnityEngine;
using System.Collections;

public class AiController : IUnit {

	float timeUntilRageMode = 1f, timeUntilEnd = 12f;
	public float timeInRageMode = 12f;

	NavMeshAgent nav;

	public Vector3 goal;

	GameObject GetNearestObject(string tag, bool self = false) {
		GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

		if (objs.Length == 0)
			throw (new System.Exception("No Object with Tag: " + tag));

		if(objs.Length == 1 && self && objs[0] == gameObject) {
			return null;

		}

		GameObject nearestObj = objs[0] == gameObject ? objs[1] : objs[0];
		foreach(GameObject obj in objs) {
			if (self && obj == gameObject)
				continue;

			if (Vector3.Distance(obj.transform.position, transform.transform.position) < Vector3.Distance(nearestObj.transform.position, transform.transform.position))
				nearestObj = obj;
		}
		return nearestObj;
	}

	// Use this for initialization
	new void Start () {
		base.Start();
		nav = GetComponent<NavMeshAgent>();
		goal = new Vector3(Random.Range(50, 144), 0, Random.Range(43, 139));//GetNearestObject("Grass");
		nav.SetDestination(goal);

		life = maxLife;
		timeUntilRageMode = Random.Range(5, 10);
		timeUntilEnd = timeInRageMode;
	}
	
	void RageMode() {
		timeUntilRageMode -= Time.deltaTime;

		if(timeUntilRageMode <= 0) {
			timeUntilEnd -= Time.deltaTime;

			GameObject nearestAI = GetNearestObject("Unit", true);
			GameObject player = GameObject.FindGameObjectWithTag("Player");

			if (nearestAI == null)
				nearestAI = player;

			if(Vector3.Distance(player.transform.position, transform.position) <= Vector3.Distance(nearestAI.transform.position, transform.position)) {
				goal = player.transform.position;
				nav.SetDestination(goal);
				//Debug.Log("Attack Player/" + name);
			} else {
				goal = nearestAI.transform.position;
				nav.SetDestination(goal);
				//Debug.Log("Attack "+nearestAI.name + "/" + name);
			}
			
			if(timeUntilEnd <= 0) {
				timeUntilEnd = timeInRageMode;
				timeUntilRageMode = Random.Range(10, 25);
				goal = Vector3.up * -1;
			}
			//Debug.Log(goal + " Rarrr, I am: " + name);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		GrassController.active.RemoveGrassInBounds(GetComponent<SphereCollider>().bounds,tag);

		RageMode();

		if (goal == Vector3.up * -1) {
			goal = new Vector3(Random.Range(50, 144), 0, Random.Range(43, 139)); //GetNearestObject("Grass");
			nav.SetDestination(goal);
		}

		if(Vector3.Distance(transform.position,goal) <= 2) {
			goal = new Vector3(Random.Range(50, 144), 0, Random.Range(43, 139));//GetNearestObject("Grass");
			nav.SetDestination(goal);
		}

	}

	public void OnTriggerStay(Collider other) {
		//Debug.Log("Collision with: " + other.tag);
		if (other.tag == "Grass") {
			Destroy(other.gameObject);


		}
		if(other.tag == "Unit") {
			//Debug.Log("Collision with Unit: " + other.name);

			GameObject targetDamage = nav.velocity.magnitude < other.GetComponent<NavMeshAgent>().velocity.magnitude ? gameObject : other.gameObject;

			Vector3 dir = transform.position - other.transform.position; //other.GetComponent<NavMeshAgent>().velocity - nav.velocity;

			float angle = 65;

			if (Vector3.Angle(transform.forward, other.transform.position - transform.position) > angle) {
				targetDamage = gameObject;
				//Debug.Log("He hit me!" + name + "/" + other.name);
			} else {
				targetDamage = other.gameObject;
				//Debug.Log("Got Him!" + name + "/" + other.name);
			}
			//Debug.Break();

			GameObject hitter = nav.velocity.magnitude < other.GetComponent<NavMeshAgent>().velocity.magnitude ? other.gameObject : gameObject;

			float damage = Vector3.Distance(nav.velocity, other.GetComponent<NavMeshAgent>().velocity);
			//Debug.Log(damage + "/" + other.GetComponent<NavMeshAgent>().velocity + "/" + targetDamage.name);


			targetDamage.GetComponent<IUnit>().life -= damage;

			Instantiate(crashPrefab, other.transform.position + dir.normalized + Vector3.up * .5f, Quaternion.FromToRotation(transform.position, other.transform.position));

			//targetDamage.GetComponent<Rigidbody>().AddForce(gameObject ? other.transform.forward * 2 : transform.forward * 2);
			targetDamage.transform.position += targetDamage == gameObject ? other.transform.forward * 1.5f : transform.forward * 1.5f;
		}

	}
}
