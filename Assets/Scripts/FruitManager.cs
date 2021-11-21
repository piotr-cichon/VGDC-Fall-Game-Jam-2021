using System;
using TMPro;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private GameObject appleHolder;
    [SerializeField] private GameObject bananaHolder;

    private void UpdateScore(GameObject holder)
    {
        TextMeshProUGUI text = holder.GetComponentInChildren<TextMeshProUGUI>();
        int score = Int32.Parse(text.text);
        text.text = (score + 1).ToString();
    }

    public void AddBanana()
    {
        UpdateScore(bananaHolder);
    }
        
    public void AddApple()
    {
        UpdateScore(appleHolder);
    }
}