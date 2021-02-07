using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : MonoBehaviour {
	public System.Action<string> EventCallback;

	public void OnRecieveEvent(string eventName) {
		EventCallback?.Invoke(eventName);
	}
}
