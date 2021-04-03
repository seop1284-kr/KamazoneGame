using UnityEngine;
using UnityEngine.EventSystems;

public class Singleton<T> where T : class, new() {
	public static T Instance {
		get {
			if (_instance == null) {
				_instance = new T();
			}

			return _instance;
		}
	}

	private static T _instance;
}

public class MonoSingleton<T> : UIBehaviour where T : MonoBehaviour {
	public static T Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<T>();

				if (_instance == null) {
					_instance = new GameObject($"MonoSingleton{typeof(T).ToString()}").AddComponent<T>();
				}
			}

			return _instance;
		}
	}

	private static T _instance;

	public virtual void Init() {
	}

	protected override void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}