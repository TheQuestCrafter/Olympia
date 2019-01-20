using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizer : MonoBehaviour {

	// Use this for initialization
	void Awake() {
        Screen.SetResolution(1200, 800, false);
	}
}
