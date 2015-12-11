using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public static CameraMovement active;

	public System.Action<bool> OnFirstPerson;


	float verticalRotation = 0;
	public float distance = 5;
	Transform target;

	public bool fixY = false, rotate = true, fixRotY = false;
	public Vector3 offset, RotOffset;
	public string RTSCamera = "--------------";
	public float minDistance = 5f, maxDistance = 15f, geschwindigkeit = 5f, zoomGeschwindigkeit;

	protected Vector3 oldPos;

	// Use this for initialization
	void Start() {
		active = this;
	}

	Quaternion ClampRotationAroundXAxis(Quaternion q) {

		//q.y /= q.w;
		//q.z /= q.w;
		q.w = 1.0f;
		q.x /= q.w;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

		angleX = Mathf.Clamp(angleX, -90, 30);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

	// Update is called once per frame
	void Update() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;


		transform.position += transform.forward * (Input.GetAxisRaw("Mouse ScrollWheel") * zoomGeschwindigkeit);
		RaycastHit info;
		if (Physics.Raycast(new Ray(transform.position, transform.forward), out info, Mathf.Infinity, 1 << 8)) {
			if (Vector3.Distance(info.point, transform.position) <= minDistance)
				transform.position -= (transform.forward * (minDistance - Vector3.Distance(info.point, transform.position)));
			else if (Vector3.Distance(info.point, transform.position) >= maxDistance)
				transform.position -= (transform.forward * (maxDistance - Vector3.Distance(info.point, transform.position)));
		}
		//Debug.Log(Vector3.Distance(info.point, transform.position));


		transform.position += Time.deltaTime * Vector3.right * (geschwindigkeit * (Input.GetAxisRaw("Sprint") != 0 ? 3 : 1) * Input.GetAxisRaw("Horizontal")) + Time.deltaTime * Vector3.forward * (geschwindigkeit * (Input.GetAxisRaw("Sprint") != 0 ? 3 : 1) * Input.GetAxisRaw("Vertical"));
		if ((transform.position.x < 0 || transform.position.x > 500) || (transform.position.z < -10 || transform.position.z > 490))
			transform.position = oldPos;
		oldPos = transform.position;
	}
}
