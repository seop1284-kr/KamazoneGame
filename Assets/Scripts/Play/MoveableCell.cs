using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableCell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler {
	private static Vector2Int originCell;
	public Vector2Int Coord;
	private bool isClicked = false;
	
	public void OnPointerDown(PointerEventData eventData) {
		isClicked = true;
		originCell = Coord;
		BoardControl.Instance.BeginDrag(Coord);
	}

	public void OnPointerUp(PointerEventData eventData) {
		if (isClicked) {
			isClicked = false;
			BoardControl.Instance.FinishDrag(originCell);
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		BoardControl.Instance.ExitDrag(Coord);
	}

	public void OnPointerEnter(PointerEventData eventData) {
		BoardControl.Instance.EnterCell(Coord);
	}
}
