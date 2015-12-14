using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {

	public static UiController active;

	public float yProgress,oProgress;
	public Image yProgressIm, oProgressIm;
	public Text dead;
	
	public bool isDead;

	// Use this for initialization
	void Start () {
		active = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(dead != null) {
			dead.enabled = isDead;
		}

		if (Input.GetButtonDown("Escape")) {
			transform.FindChild("UILayer").FindChild("Escape").gameObject.SetActive(true);
			Time.timeScale = 0f;
		}

		if (yProgressIm != null) {
			yProgressIm.fillAmount = yProgress;
			oProgressIm.fillAmount = oProgress;


			if(yProgress <= 0) {
				transform.FindChild("UILayer").FindChild("Victory").gameObject.SetActive(true);
				Time.timeScale = 0f;
			}
			if (oProgress <= 0) {
				transform.FindChild("UILayer").FindChild("Gameover").gameObject.SetActive(true);
				Time.timeScale = 0f;
			}

		}
	}

	public void Exit() {
		Application.Quit();
	}

	public void Continue() {
		Time.timeScale = 1;
		transform.FindChild("UILayer").FindChild("Escape").gameObject.SetActive(false);
	}

	public void GotoSelectLevel() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("_SELECTLEVEL_");
	}
	public void SelectLevel(int level) {
		Time.timeScale = 1f;
		SceneManager.LoadScene("_LEVEL0" + level + "_");
	}
}
