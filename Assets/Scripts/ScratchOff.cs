using UnityEngine;
using System.Collections;

public class ScratchOff : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	// Update is called once per frame
	void Update () {

		foreach (Touch touch in Input.touches) { 

			ray = Camera.main.ScreenPointToRay (touch.position);

			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {

				if (hit.transform.gameObject.tag == "ScratchOff" && touch.phase == TouchPhase.Began) {
					Destroy (hit.transform.gameObject);
				}

				if (hit.transform.gameObject.tag == "ScratchOff" && touch.phase == TouchPhase.Moved) {
					Destroy (hit.transform.gameObject);
				}
			}
		}
	}
}