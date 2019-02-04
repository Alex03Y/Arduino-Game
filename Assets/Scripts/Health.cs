using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Vector2 _maxHealthScatter = new Vector2(50, 200);
    private float _maxHealth;
    public float DebugCurHealth;
    
    [Serializable]
    public class UnityFloatEvent : UnityEvent<float> { }
    public UnityFloatEvent OnHealthChanged;
    public UnityEvent      OnDeath;

    private float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (_currentHealth.Equals(value)) return;
            if (value.Equals(0)) OnDeath?.Invoke();
            _currentHealth = value;
            DebugCurHealth = _currentHealth;
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    private void Awake()
    {
        _maxHealth = Random.Range(_maxHealthScatter.x, _maxHealthScatter.y);
        CurrentHealth = _maxHealth;
    }

    public void AddHealth(float amount) => CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, _maxHealth);
    public void RemoveHealth(float amount) => CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, _maxHealth);
}
