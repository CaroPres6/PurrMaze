using UnityEngine;
using UnityEngine.UI;

public class VieJoueur : MonoBehaviour
{
    public int vieMax = 100;
    public int currentVie;

    public Slider sliderVie;

    void Start()
    {
        currentVie = vieMax;
        sliderVie.maxValue = vieMax;
        sliderVie.value = currentVie;
    }

    public void TakeDamage(int damage)
    {
        currentVie -= damage;

        if (currentVie < 0)
        {
            currentVie = 0;
        }
        sliderVie.value = currentVie;
     }
}