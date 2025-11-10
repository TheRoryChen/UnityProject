using UnityEngine;

public class Health : MonoBehaviour {
    public int maxHP = 50; public int Current { get; private set; }
    public System.Action onDeath, onChange;
    void Awake(){ Current = maxHP; }
    public void Damage(int dmg){
        if (Current <= 0) return;
        Current = Mathf.Max(0, Current - dmg); onChange?.Invoke();
        if (Current == 0) onDeath?.Invoke();
    }


}
