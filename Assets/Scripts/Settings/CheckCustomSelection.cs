using UnityEngine;
using UnityEngine.UI;

public class CheckCustomSelection : MonoBehaviour
{
    [SerializeField]
    private Slider _otherSlider;

    public void Check()
    {
        Debug.Log("ChangedChecked");
        if (GetComponentInParent<Slider>().value * _otherSlider.value % 2 == 1)
        {
            _otherSlider.value++;
            Debug.Log("ChangedValue");
        }
    }
}
