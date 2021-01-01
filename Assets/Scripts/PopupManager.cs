using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager> {
    private Dictionary<string, PopupBase> _popupCache = new Dictionary<string, PopupBase>();
    private Stack<PopupBase> _popups = new Stack<PopupBase>();

    public bool IsOpenedPopup => _popups.Count > 0;
    
    void Start() {
        var popups = GetComponentsInChildren<PopupBase>(true);
        foreach (var popup in popups) {
            _popupCache.Add(popup.name, popup);
        }

    }

    public void Show(string popupName) {
        if (_popupCache.ContainsKey(popupName)) {
            _popups.Push(_popupCache[popupName]);
            _popups.Peek().Show();
        } else {
            Debug.LogError(popupName + " is not defined");
        }
    }
    
    public void Close() {
        Debug.Assert(_popups.Count > 0, "_popups.Count > 0");
        var popup = _popups.Peek();
        popup.Hide();

        _popups.Pop();
    }
}
