using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkillData", menuName = "Demo/SkillData ")]
public class SkillData : ScriptableObject
{
    public int id;
    public string description;
    public int targetMax;
    public Vector3 castCenter;
    public float castRadius;
    public float castHeight;
    public float damageCoefficient;

}
