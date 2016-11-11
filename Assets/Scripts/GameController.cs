using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public string coupon_value = "";
	public TextMesh couponValueText;
	public GameObject wordObject;
	public GameObject textReco;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (!(coupon_value.Equals (""))) {
			wordObject.SetActive (false);
			textReco.SetActive (false);
		}
	}

	public void setCouponValue(string value){
		Debug.Log ("This is the value: " + value);
		coupon_value = value;
		couponValueText.text = coupon_value;
		Debug.Log ("This is the couponValue: " + couponValueText.text);
	}

	public string getCouponValue(){
		return coupon_value;
	}

}
