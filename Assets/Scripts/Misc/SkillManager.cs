using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    [Header("Stat Holder")]
    [SerializeField] public float damage;
    [SerializeField] public float health;
    [SerializeField] public float moveSpeedInRange;
    [SerializeField] public float moveSpeedOutRange;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
