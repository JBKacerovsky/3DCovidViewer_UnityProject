using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    private bool toggle = false;
    [SerializeField] private Color colorOn = new Color(0.8f, 0.8f, 1); 
    [SerializeField] private Color colorOff = new Color(1, 1, 1);

    public void toggleColor()
    {
        toggle = !toggle;

        if (toggle)
        {
            GetComponent<Image>().color = colorOn;
        }
        else
        {
            GetComponent<Image>().color = colorOff;
        }
    }

    public void setColorOff()
    {
        toggle = false;
        GetComponent<Image>().color = colorOff;
    }
}
