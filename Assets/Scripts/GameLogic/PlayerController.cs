using UnityEngine;
using System.Collections;

public class PlayerController : IUnit {

	CharacterController cc;
	Rigidbody rg;

	public float speed = 5f, rotSpeed = 5f;

	// Use this for initialization
	new void Start () {
		base.Start();
		cc = GetComponent<CharacterController>();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UiController.active.isDead = dead;

		if (dead)
			return;

		cc.SimpleMove(transform.forward * speed);

		rg = GetComponent<Rigidbody>();

		//rg.a
		transform.Rotate(new Vector3(0,rotSpeed,0) * Input.GetAxis("Horizontal"));

		GrassController.active.RemoveGrassInBounds(GetComponent<SphereCollider>().bounds,tag);
	}



	public void OnTriggerEnter(Collider other) {
		//Debug.Log("Collision with: " + other.tag);
		if (other.tag == "Grass") {
			Destroy(other.gameObject);


		}
		if (other.tag == "Unit") {

			//Debug.Log("Collision with Unit: " + other.name);

			//GameObject targetDamage = cc.velocity.magnitude < other.GetComponent<NavMeshAgent>().velocity.magnitude ? gameObject : other.gameObject;
			//GameObject hitter = nav.velocity.magnitude < other.GetComponent<CharacterController>().velocity.magnitude ? other.gameObject : gameObject;

			Vector3 dir = (transform.position - other.transform.position);
			/*
			Ray ray = new Ray(transform.position, transform.rotation.eulerAngles);
			Debug.DrawRay(transform.position, transform.rotation.eulerAngles, Color.red);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100)) {
				if (hit.transform == transform) {
					targetDamage = gameObject;
					Debug.Log("He hit me!" + name + "/" + other.name);
				} else {
					targetDamage = other.gameObject;
					Debug.Log("Got Him!" + name + "/" + other.name);
				}

			}*/
			float angle = 65;

			//if (Vector3.Angle(transform.forward, other.transform.position - transform.position) > angle) {
			//	targetDamage = gameObject;
				//Debug.Log("He hit me!" + name + "/" + other.name);
			//} else {
			//	targetDamage = other.gameObject;
				//Debug.Log("Got Him!" + name + "/" + other.name);
			//}
			//Debug.Log(Vector3.Angle(transform.forward, other.transform.position - transform.position));
			//Debug.Break();

			float damage = Vector3.Distance(cc.velocity, -other.GetComponent<NavMeshAgent>().velocity) <= 0.5f ? Vector3.Distance(cc.velocity, -other.GetComponent<NavMeshAgent>().velocity) : Vector3.Distance(cc.velocity, other.GetComponent<NavMeshAgent>().velocity);
			//Debug.Log(damage + "/" + other.GetComponent<NavMeshAgent>().velocity + "/" + targetDamage.name);



			Instantiate(crashPrefab, other.transform.position + dir.normalized, Quaternion.FromToRotation(transform.position, other.transform.position));


			other.GetComponent<IUnit>().life = 0;
			life -= 2f;
			//targetDamage.GetComponent<Rigidbody>().AddForce(gameObject ? other.transform.forward * 200 : transform.forward * 200);
			/*Debug.Log(Vector3.Angle(transform.forward, other.transform.forward)+"/" + Vector3.Angle(transform.forward, other.transform.position - transform.position));
			if (Vector3.Angle(transform.forward, other.transform.forward) > 165) {
				other.GetComponent<NavMeshAgent>().velocity += transform.forward * 1.5f + transform.right * Mathf.Sign(Vector3.Angle(transform.forward, other.transform.forward)) * 2;
				//other.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
				//GetComponent<Rigidbody>().AddForce(other.transform.forward * 1000f,ForceMode.Impulse);
				cc.SimpleMove(other.transform.forward * 1.5f + other.transform.right * Mathf.Sign(Vector3.Angle(transform.forward, other.transform.forward)) * 2);
				other.GetComponent<IUnit>().life -= damage;
				life -= damage;
				Debug.Log("Frontal");
			} else {
				targetDamage.GetComponent<IUnit>().life -= damage;
				
				other.GetComponent<NavMeshAgent>().velocity += transform.forward * 1.5f + transform.right * Mathf.Sign(Vector3.Angle(transform.forward, other.transform.forward)) * 2;

				//targetDamage.transform.position += targetDamage == gameObject ? other.transform.forward * 1.5f + other.transform.right * Mathf.Sign(Vector3.Angle(transform.forward, other.transform.forward)) * 5 : transform.forward * 1.5f + transform.right * Mathf.Sign(Vector3.Angle(transform.forward, other.transform.forward)) * 5; //targetDamage.GetComponent<Rigidbody>().AddForce(dir.normalized * 2f);//transform.position += dir.normalized * 0.5f;
			}*/
		} else if(other.tag != "Ground" && other.name != "Player") {
			life = 0;
		}
	}
}
