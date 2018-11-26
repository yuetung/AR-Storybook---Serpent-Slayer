/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/
using System.Collections;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES
		private bool[] setActiveStorage;
		private bool[] finalSetActiveStorage;

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
			Transform[] transforms = GetComponentsInChildren<Transform> (true);
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			AudioSource[] audioComponents = GetComponentsInChildren<AudioSource>(true);
			Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

			for (int i=0; i<transforms.Length; i++)
			{
				if (!transforms[i].gameObject.GetInstanceID().Equals(gameObject.GetInstanceID ())) {
					transforms[i].gameObject.SetActive (finalSetActiveStorage[i]);
					print (i);
				}
			}
			finalSetActiveStorage = null;

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

			// Enable Audios:
			foreach (AudioSource component in audioComponents)
			{
				component.enabled = true;
			}

			// Enable Canvas:
			foreach (Canvas component in canvasComponents)
			{
				component.enabled = true;
			}

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
			StartCoroutine (Disabler());

        }

        #endregion // PRIVATE_METHODS

		IEnumerator Disabler(){
			yield return new WaitForSeconds (0);
			Transform[] transforms = GetComponentsInChildren<Transform> (true);
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			AudioSource[] audioComponents = GetComponentsInChildren<AudioSource>(true);
			Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

			setActiveStorage=new bool[transforms.Length];
			for (int i=0; i<transforms.Length; i++)
			{
				if (!transforms[i].gameObject.GetInstanceID().Equals(gameObject.GetInstanceID ())) {
					setActiveStorage [i] = transforms[i].gameObject.activeSelf;
					transforms[i].gameObject.SetActive (false);
				}
			}
			if (finalSetActiveStorage==null) {
				finalSetActiveStorage = setActiveStorage;
			}
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

			// Disable Audios:
			foreach (AudioSource component in audioComponents)
			{
				component.enabled = false;
			}

			// Disable Canvas:
			foreach (Canvas component in canvasComponents)
			{
				component.enabled = false;
			}

			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}
    }


}
