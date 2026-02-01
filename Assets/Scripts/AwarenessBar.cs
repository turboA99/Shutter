using Managers;
using UnityEngine;
using UnityEngine.UI;

public class AwarenessBar : MonoBehaviour
{
    [SerializeField] private Image bar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AwarenessManager.instance.OnAwarenessChange.AddListener(UpdateBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBar(float newValue)
    {
        bar.fillAmount = newValue / 100;
    }
}
