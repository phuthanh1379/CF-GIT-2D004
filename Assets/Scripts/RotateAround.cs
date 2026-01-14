using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RotateAround : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1.5f)] private float speed;
    [SerializeField, Tooltip("Point where object rotates around")] private Transform axisPoint;
    [SerializeField] private List<CharacterProfile> dataList = new();
    [SerializeField] private List<Sprite> spriteList = new();

    [Header("Integer")]
    public int a;
    public int b;
    public int c;

    [Header("Float")]
    public float d;
    public float e;

    void Update()
    {
        transform.RotateAround(axisPoint.position, new Vector3(0f, 0f, 1f), speed);
        SceneManager.LoadScene("scene1", LoadSceneMode.Additive);
    }
}

public enum CharacterClass
{
    Warrior,
    Archer,
    Baker,
    Assassin,
    Villager
}

[Serializable]
public class CharacterProfile
{
    public CharacterClass characterClass;
    public int health;
    public float speed;
    public int damage;
    public string name;
}