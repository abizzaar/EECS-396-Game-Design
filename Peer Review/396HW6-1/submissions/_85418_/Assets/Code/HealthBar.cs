using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;

        public void doDamage(int damage)
        {
            slider.value = slider.value - damage;
        }
    }
}