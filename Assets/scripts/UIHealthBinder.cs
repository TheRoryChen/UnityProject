using UnityEngine;
using UnityEngine.UI;

public class UIHealthBinder : MonoBehaviour {
    public Health target;
    public Slider bar;

    void Start() {
        if (!target || !bar) return;
        bar.minValue = 0;
        bar.maxValue = target.maxHP;
        bar.value   = target.maxHP;

        // Update when HP changes
        target.onChange += () => {
            bar.value = target.Current;   // uses Health.Current getter
        };
    }
}