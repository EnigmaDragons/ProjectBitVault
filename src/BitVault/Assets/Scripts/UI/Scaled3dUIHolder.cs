using System;
using System.Collections;
using UnityEngine;

public class Scaled3dUIHolder : MonoBehaviour
{
    [SerializeField] private float pixelsPerScale = 10;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject thingToScale;

    private void OnEnable()
    {
        StartCoroutine(UpdateScale());
    }

    private IEnumerator UpdateScale()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            var rect = RectTransformToScreenSpace(rectTransform);
            var xScale = rect.width / pixelsPerScale;
            var yScale = rect.height / pixelsPerScale;
            var scale = 0f;
            if (xScale > yScale)
                scale = yScale;
            else if (xScale < yScale)
                scale = xScale;
            thingToScale.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        float x = transform.position.x + transform.anchoredPosition.x;
        float y = Screen.height - transform.position.y - transform.anchoredPosition.y;

        return new Rect(x, y, size.x, size.y);
    }
}
