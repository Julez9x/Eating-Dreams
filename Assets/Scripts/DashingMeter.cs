using UnityEngine;
using UnityEngine.UI;

public class DashingMeter : MonoBehaviour
{
    public Image staminaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        staminaBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.fillAmount = Mathf.MoveTowards(staminaBar.fillAmount, 1, Time.deltaTime / 3f);
    }

    public void EmptyMeter() 
    {
        staminaBar.fillAmount = 0;
    }
}
