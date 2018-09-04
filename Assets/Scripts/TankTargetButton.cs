using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTargetButton : MonoBehaviour {

    public enum TargetVariable { Rotation, Angle, Power };
    public enum TargetDirection { Plus, Minus };
    public TargetVariable targetVariable;
    public TargetDirection targetDirection;

    public Tanks.Target BuildTargetDelta() {
        float rotation = 0f;
        float angle = 0f;
        float power = 0f;

        float directionMultiplier = targetDirection == TargetDirection.Plus ? 1f : -1f;

        switch (targetVariable) {
            case TargetVariable.Rotation:
                rotation += 0.5f * directionMultiplier;
                break;
            case TargetVariable.Angle:
                angle += 0.5f * directionMultiplier;
                break;
            case TargetVariable.Power:
                power += 0.01f * directionMultiplier;
                break;
        }

        return new Tanks.Target(rotation, angle, power);
    }
}