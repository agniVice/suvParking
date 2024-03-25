using UnityEngine;
using UnityEngine.UI;

public class GearBox : MonoBehaviour
{
    public int GearShift = 1;
    public Slider GearShiftSlider;

    private void Start()
    {
        GearShiftSlider = FindObjectOfType<Slider>();
        GearShiftSlider.onValueChanged.AddListener(delegate { SetGearShift(); });
    }
    public void SetGearShift()
    {
        GearShift = Mathf.RoundToInt(GearShiftSlider.value);
    }
}
