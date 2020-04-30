﻿using UnityEngine;

public class FieldOfViewSwitch : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayoutMode layout;
    [SerializeField] private float tallFieldOfView;
    [SerializeField] private float widFieldOfView;

    private bool isTall;
    
    private void Update()
    {
        if (isTall != layout.IsTall)
            camera.fieldOfView = layout.IsTall ? tallFieldOfView : widFieldOfView;
    }
}
