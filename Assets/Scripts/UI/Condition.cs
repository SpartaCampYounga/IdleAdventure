using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateConditionUI(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        text.text = $"{currentValue} / {maxValue}";
    }
}
