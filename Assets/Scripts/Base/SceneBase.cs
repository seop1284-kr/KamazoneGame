using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : HandlerBase
{
	public virtual void Entered() {
		SceneManager.Instance.CurrentScene = this;
	}
}
