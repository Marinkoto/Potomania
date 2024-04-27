using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAiming : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform aimTransform;

    void Update()
    {
        if(Time.time != 0)
        {
            Aim();
        }
    }

    private void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }
        aimTransform.localScale = localScale;
    }
}
