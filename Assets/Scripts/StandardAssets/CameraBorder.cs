using UnityEngine;
using System.Collections;
using StandardAssets;

[ExecuteInEditMode]
public class CameraBorder : MonoBehaviour {

	public Texture border;

	void OnGUI() {
		//Debug.Log(Camera.main.rect.width);
		GUI.depth = 10;
		GUI.color = Color.white;
		if (!CameraSize.letterbox) {
			GUI.DrawTexture(new Rect(0, 0, Camera.main.pixelRect.x, GetComponent<Camera>().pixelRect.height), border);
			GUI.DrawTexture(new Rect(Camera.main.pixelRect.width + Camera.main.pixelRect.x, 0, GetComponent<Camera>().pixelRect.width - Camera.main.pixelRect.width, GetComponent<Camera>().pixelRect.height), border);
		} else {
			GUI.DrawTexture(new Rect(0, 0, Camera.main.pixelRect.width,Camera.main.pixelRect.y), border);
			GUI.DrawTexture(new Rect(0,Maths.PixelToRect(Camera.main.pixelRect.y), Camera.main.pixelRect.width, Camera.main.pixelRect.y), border);
		}


	}
}
