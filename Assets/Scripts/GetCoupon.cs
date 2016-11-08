using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetCoupon : MonoBehaviour {

	string coupon_value;
	public GameObject image;

	// Use this for initialization
	void Start () {
		StartCoroutine (loadCouponValue ("qwertyuiop"));
//		StartCoroutine (postScore ());
	}

	// Update is called once per frame
	void Update () {

	}

//	public void post(){
//		StartCoroutine (postScore ());
//	}
//
//	IEnumerator postScore() {
//		WWWForm form = new WWWForm ();
//		form.AddField("url", url);
//		WWW postRequest = new WWW("http://localhost:3000/coupons", form);
//		yield return postRequest;
//	}

	IEnumerator loadCouponValue(string coupon_key) {
		using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/coupons.json?coupon_key=" + coupon_key))
		{
			yield return request.Send();

			if (request.isError) {
				Debug.Log(request.error);
			} else {
				JSONObject json = new JSONObject(request.downloadHandler.text);
				coupon_value = json ["coupon_value"].ToString ();
				coupon_value = coupon_value.Substring (1, coupon_value.Length - 2);
				Debug.Log (coupon_value);
			}
		}
	}
}