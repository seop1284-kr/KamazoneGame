using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager> {
    private Dictionary<string, PopupBase> _popupCache = new Dictionary<string, PopupBase>();
    private Stack<PopupBase> _popups = new Stack<PopupBase>();

    public bool IsOpenedPopup => _popups.Count > 0;

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

    public void Add(string popupName, PopupBase popup) {
        _popupCache.Add(popupName, popup);
    }
    
    public void Remove(string popupName) {
        _popupCache.Remove(popupName);
    }
}
