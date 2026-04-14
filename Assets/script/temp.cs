using TMPro;
using UnityEngine;

public class temp : MonoBehaviour
{

    public bool rightOrLeft = false;

    public TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rightOrLeft)
        {
            float temp = Input.GetAxis("thumbStick Y");
            text.SetText(temp.ToString());
        }
        else
        {
            float temp = Input.GetAxis("Thumstick X");
            text.SetText(temp.ToString());
        }
    }
}
