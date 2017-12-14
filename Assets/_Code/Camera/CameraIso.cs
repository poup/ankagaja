using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIso : MonoBehaviour {

	void Awake () {
		transform.rotation = Quaternion.Euler(45, 0, 0);
	}
}
