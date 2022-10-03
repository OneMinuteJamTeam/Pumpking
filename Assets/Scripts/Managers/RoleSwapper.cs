using UnityEngine;

public class RoleSwapper : Singleton<RoleSwapper>
{
    public enum Role
    {
        Chaser,
        Escapee
    }

    public Role PumpkinRole { get; private set; }
    public Role ScarecrowRole { get; private set; }

    public void SwapRoles()
    {
        if (PumpkinRole == Role.Chaser)
            SetRoles(Role.Escapee, Role.Chaser);
        else
            SetRoles(Role.Chaser, Role.Escapee);
    }

    protected override void Awake()
    {
        base.Awake();
        SetRoles(Role.Chaser, Role.Escapee);
    }

    private void SetRoles(Role pumpkin, Role scarecrow)
    {
        PlayerPrefs.DeleteAll();
        if (pumpkin == Role.Escapee)
            PlayerPrefs.SetInt("PumpkinEscaping", 1);
        else
            PlayerPrefs.SetInt("PumpkinEscaping", 0);

        if (scarecrow == Role.Escapee)
            PlayerPrefs.SetInt("ScarecrowEscaping", 1);
        else
            PlayerPrefs.SetInt("ScarecrowEscaping", 0);

        PumpkinRole = pumpkin;
        ScarecrowRole = scarecrow;
    }
}
