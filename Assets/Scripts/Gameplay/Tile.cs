using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Color _hiddenColour, _defaultColour;
    private bool _isHidden = false;
    /// <summary>
    /// Flag to show if the tile is going to be hidden soon
    /// </summary>
    internal bool hidingSoon;
    
    internal void SetColour(Color newColour)
    {
        GetComponent<Image>().color = newColour;
        _defaultColour = newColour;
    }

    internal void Hide()
    {
        GetComponent<Image>().color = _hiddenColour;
        _isHidden = true;
    }

    internal void Show()
    {
        GetComponent<Image>().color = _defaultColour;
        _isHidden = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isHidden)
        {
            Show();
            SelectionManager.onTileSelect.Invoke(this);
        }
    }

    internal Color GetHiddenColour()
    {
        return _hiddenColour;
    }
}
