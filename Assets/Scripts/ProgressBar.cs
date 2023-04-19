using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    private float percent;


    public void UpdateBar(float current, float max)
    {
        percent = current / max;
        _progressBar.fillAmount = percent;

    }
}
