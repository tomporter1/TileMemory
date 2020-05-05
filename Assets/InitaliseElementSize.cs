using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class InitaliseElementSize : MonoBehaviour
{
    [SerializeField]
    private RectTransform _refObj;
    [SerializeField]
    private VerticalLayoutGroup _vertGroup;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        float spacing = _vertGroup.spacing;
        float height = _refObj.rect.height;
        int numOfElements = transform.parent.childCount;

        if (height < 0)
            height *= -1;

        GetComponent<LayoutElement>().preferredHeight = (height - (spacing * numOfElements)) / numOfElements;
        GetComponent<LayoutElement>().preferredWidth = _refObj.rect.width;
    }
}
