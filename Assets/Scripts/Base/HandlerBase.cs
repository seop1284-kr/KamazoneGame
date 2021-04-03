using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class HandlerBase : MonoBehaviour{
	public virtual void OnEscape() {
		Debug.Log("OnEscape");
	}
}
