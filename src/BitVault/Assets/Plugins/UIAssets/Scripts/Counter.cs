using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private Text counterText;
    public Slider Slider;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<Text>();
        Debug.Log(transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = Slider.value.ToString("0");
    }
}
