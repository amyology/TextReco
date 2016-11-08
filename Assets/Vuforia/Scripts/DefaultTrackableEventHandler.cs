/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
	{
		string coupon_value = "This is slow";
		public TextMesh couponValueText;

		#region PRIVATE_MEMBER_VARIABLES

		private TrackableBehaviour mTrackableBehaviour;

		#endregion // PRIVATE_MEMBER_VARIABLES



		#region UNTIY_MONOBEHAVIOUR_METHODS

		void Start()
		{
			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}

		}

		#endregion // UNTIY_MONOBEHAVIOUR_METHODS



		#region PUBLIC_METHODS

		/// <summary>
		/// Implementation of the ITrackableEventHandler function called when the
		/// tracking state changes.
		/// </summary>
		public void OnTrackableStateChanged(
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
				newStatus == TrackableBehaviour.Status.TRACKED ||
				newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				OnTrackingFound();

			}
			else
			{
				OnTrackingLost();
			}
		}

		#endregion // PUBLIC_METHODS



		#region PRIVATE_METHODS


		private void OnTrackingFound()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

			// Enable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = true;
			}

			// Enable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = true;
			}

			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

			StartCoroutine(loadCouponValue (mTrackableBehaviour.TrackableName));
//			StartCoroutine(loadCouponValue ("qwertyuiop"));
			couponValueText.text = coupon_value;
		}


		private void OnTrackingLost()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

			// Disable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}

			// Disable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = false;
			}

			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}

		#endregion // PRIVATE_METHODS

		IEnumerator loadCouponValue(string coupon_key) {
			using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/coupons/" + coupon_key + ".json"))
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
}
