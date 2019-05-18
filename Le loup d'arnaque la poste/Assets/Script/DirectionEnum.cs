using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionEnum
{
    Top,
    Bottom,
    Left,
    Right


}
public static class DirectionEnumMethods
{
    public static DirectionEnum GetDirection(Vector2 movement)
    { 
            Vector2 right = new Vector2(1, 0);
            float angle = Vector2.SignedAngle(movement, right);
            if (angle > -45 && angle <= 45)
                return DirectionEnum.Right;
            else if (angle > 45 && angle <= 135)
                return DirectionEnum.Bottom;
            else if (angle > -135 && angle <= -45)
                return DirectionEnum.Top;
            else
                return DirectionEnum.Left;
    }

    public static Quaternion GetQuaternionFromDirection(DirectionEnum dir)
    {
        switch (dir)
        {
            case DirectionEnum.Bottom:
                return Quaternion.Euler(0,0,180);
            case DirectionEnum.Left:
                return Quaternion.Euler(0, 0, 90);
            case DirectionEnum.Top:
                return Quaternion.Euler(0, 0, 0);
            case DirectionEnum.Right:
                return Quaternion.Euler(0, 0, -90);
            default:
                return Quaternion.identity;
        }
    }
}

