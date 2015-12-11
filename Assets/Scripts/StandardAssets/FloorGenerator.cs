using UnityEngine;
using System;
using StandardAssets;
using UnityEditor;

public class FloorGenerator : MonoBehaviour {

	MeshFilter mf;

	public MeshCreator ver;

	FloorTypes oldFloor;

	private string verName;

	public enum FloorTypes {
		FloorNormal,
		FloorUp,
		FloorDown
	}

	public FloorTypes floor;

	GameObject[] verts;

	public GameObject vertPrefab;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="floor">FloorUp, FloorNormal, FloorDown</param>
	void CreateFloor() {
		switch (floor) {
			case FloorTypes.FloorUp:
				ver = new FloorUpMeshCreator();
				Debug.Log(floor);
				return;
			case FloorTypes.FloorDown:
				ver = new FloorDownMeshCreator();
				Debug.Log(floor);
				return;
			case FloorTypes.FloorNormal:
				ver = new FloorNormalMeshCreator();
				Debug.Log(floor);
				return;
		}
		Debug.LogError("Can't create floor: " + floor + " !");

    }


	// Use this for initialization
	void Start() {
		CreateFloor();
		oldFloor = floor;

		//int it = 0;
		//verts = new GameObject[ver.Vertices.Length];
		//foreach (Vector3 pos in ver.Vertices) {
		//	verts[it] = Instantiate(vertPrefab);//new GameObject("vert" + it);
		//	verts[it].name = "vert" + it;
        //     verts[it].transform.position = pos;
		//	verts[it].transform.SetParent(transform, false);
		//
		//	it++;
		//}
		
		mf = GetComponent<MeshFilter>();

		mf.mesh.Clear();
		mf.mesh.vertices = ver.Vertices;
		mf.mesh.triangles = ver.Triangles;
		mf.mesh.uv = ver.UVs;
		mf.mesh.RecalculateNormals();
		GetComponent<MeshCollider>().sharedMesh = mf.mesh;
	}

	// Update is called once per frame
	void Update() {
		if(oldFloor != floor) {
			CreateFloor();
			oldFloor = floor;
			mf.mesh.Clear();
			mf.mesh.vertices = ver.Vertices;
			mf.mesh.triangles = ver.Triangles;
			mf.mesh.uv = ver.UVs;
			mf.mesh.RecalculateNormals();
			GetComponent<MeshCollider>().sharedMesh = mf.mesh;
		}


		//Vector3[] vertes = new Vector3[30];

		//int it = 0;

		//foreach (GameObject pos in verts) {
		//	vertes[it] = pos.transform.position;

		//	it++;
		//}

		//mf.mesh.vertices = vertes;

	}

}