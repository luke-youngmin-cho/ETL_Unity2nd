using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SwordMan : MonoBehaviour
{
    public int lv
    {
        get => _lv;
        set => _lv = value;
    }
    private int _lv;

    public ISword sword
    {
        get => _sword;
        set => _sword = value;
    }
    private ISword _sword;
    private int _a = 3;

    public void UseSkill()
    {
        _sword.Skill();
    }

}

public interface ISword
{
    void Skill();
}

public interface ISwordNoob : ISword
{
}
public interface ISwordIntermidiate : ISword
{
}
public interface ISwordMaster : ISword
{
}

public interface ISwordLegend : ISword
{
}