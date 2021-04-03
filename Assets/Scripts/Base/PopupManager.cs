using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager> {
    public PopupItem TopPopup => _popups.Peek();
    
    private Dictionary<string, PopupBase> _popupCache = new Dictionary<string, PopupBase>();
    private Stack<PopupItem> _popups = new Stack<PopupItem>();

    public class PopupItem {
        public PopupItem(PopupBase p, Action<object> c) {
            popupBase = p;
            callback = c;
        }
        public PopupBase popupBase;
        public System.Action<object> callback;
    }

    public bool IsOpenedPopup => _popups.Count > 0;

    public void Show(string popupName, object param = null, Action<object> callback = null) {
        if (_popupCache.ContainsKey(popupName)) {
            _popups.Push(new PopupItem(_popupCache[popupName], callback));
            _popups.Peek().popupBase.Show();
            _popups.Peek().popupBase.Enter(param);
        } else {
            Debug.LogError(popupName + " is not defined");
        }
    }
    
    public void Close(object p = null) {
        Debug.Assert(_popups.Count > 0, "_popups.Count > 0");
        var popup = _popups.Peek();
        popup.popupBase.Hide();
        popup.callback?.Invoke(p);

        _popups.Pop();
    }

    public void Add(string popupName, PopupBase popup) {
        _popupCache.Add(popupName, popup);
    }
    
    public void Remove(string popupName) {
        _popupCache.Remove(popupName);
    }
}
