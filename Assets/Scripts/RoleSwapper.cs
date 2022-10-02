using UnityEngine;

public class RoleSwapper : Singleton<RoleSwapper>
{
    public enum Role
    {
        Chaser,
        Escapee
    }
    public enum GameClass {
        Pumpkin,
        Scarecrow
    }

    public Role P1Role { get; private set; }
    public Role P2Role { get; private set; }
    public GameClass P1Class { get; private set; }
    public GameClass P2Class { get; private set; }
    public void SwapRoles()
    {
        if (P1Role == Role.Chaser)
            SetRoles(Role.Escapee, Role.Chaser);
        else
            SetRoles(Role.Chaser, Role.Escapee);
    }

    public void SwapClasses() {
        if(P1Class == GameClass.Pumpkin)
            SetClasses(GameClass.Scarecrow, GameClass.Pumpkin);
        else
            SetClasses(GameClass.Pumpkin,GameClass.Scarecrow);
    }

    protected override void Awake()
    {
        base.Awake();
        SetRoles(Role.Chaser, Role.Escapee);
        SetClasses(GameClass.Pumpkin, GameClass.Scarecrow);
    }

    private void SetRoles(Role _p1Role, Role _p2Role)
    {
        //PlayerPrefs.DeleteAll();
        Debug.Log("set roles called with:"+_p1Role.ToString()+","+_p2Role.ToString());
        if (_p1Role == Role.Escapee)
            PlayerPrefs.SetInt("P1Escaping", 1);
        else
            PlayerPrefs.SetInt("P1Escaping", 0);

        if (_p2Role == Role.Escapee)
            PlayerPrefs.SetInt("P2Escaping", 1);
        else
            PlayerPrefs.SetInt("P2Escaping", 0);

        P1Role = _p1Role;
        P2Role = _p2Role;
    }

    private void SetClasses(GameClass _p1Class, GameClass _p2Class) {
        Debug.Log("set classes called with:" + _p1Class.ToString()+", "+_p2Class.ToString());
        PlayerPrefs.DeleteAll();
        if (_p1Class == GameClass.Pumpkin)
            PlayerPrefs.SetInt("P1Pumpkin", 1);
        else
            PlayerPrefs.SetInt("P1Pumpkin", 0);

        if (_p2Class == GameClass.Pumpkin)
            PlayerPrefs.SetInt("P2Pumpkin", 1);
        else
            PlayerPrefs.SetInt("P2Pumpkin", 0);

        P1Class = _p1Class;
        P2Class = _p2Class;
    }


}
