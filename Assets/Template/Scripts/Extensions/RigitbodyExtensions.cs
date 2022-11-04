using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Rigitbody‚ÌŠg’£ƒƒ\ƒbƒh‚ğ‚à‚Á‚Ä‚¢‚éScript
/// </summary>
public static class RigitbodyExtensions
{
    public static Rigidbody ChangeConstraints(this Rigidbody rb,Freeze type)
    {
        var constraints = (RigidbodyConstraints)type;
        rb.constraints = constraints;
        return rb;
    }
}
public enum Freeze
{ 
    [Tooltip("‘S‰ğœ")]
    None = 0,

    [Tooltip("PositionX‚ğ~‚ß‚é")]
    PosX = 2,

    [Tooltip("PositionY‚ğ~‚ß‚é")]
    PosY = 4,

    [Tooltip("PositionX‚ÆY‚ğ~‚ß‚é")]
    PosXY = 6,

    [Tooltip("PositionZ‚ğ~‚ß‚é")]
    PosZ = 8,

    [Tooltip("PositionX‚ÆZ‚ğ~‚ß‚é")]
    PosXZ = 10,

    [Tooltip("PositionY‚ÆZ‚ğ~‚ß‚é")]
    PosYZ = 12,

    [Tooltip("Position‘S‚Ä‚ğ~‚ß‚é")]
    Pos = 14,

    [Tooltip("RotationX‚ğ~‚ß‚é")]
    RotX = 16,

    [Tooltip("PositionX‚ÆRotationX‚ğ~‚ß‚é")]
    PosXRotX = 18,

    [Tooltip("PositionY‚ÆRotationX‚ğ~‚ß‚é")]
    PosYRotX = 20,

    [Tooltip("PositionXY‚ÆRotationX‚ğ~‚ß‚é")]
    PosXYRotX = 22,

    [Tooltip("PositionZ‚ÆRotationX‚ğ~‚ß‚é")]
    PosZRotX = 24,

    [Tooltip("PositionXZ‚ÆRotationX‚ğ~‚ß‚é")]
    PosXZRotX = 26,

    [Tooltip("PositionYZ‚ÆRotationX‚ğ~‚ß‚é")]
    PosYZRotX = 28,

    [Tooltip("Position‘S‚Ä‚ÆRotationX‚ğ~‚ß‚é")]
    PosRotX = 30,

    [Tooltip("RotationY‚ğ~‚ß‚é")]
    RotY = 32,

    [Tooltip("PositionX‚ÆRotationY‚ğ~‚ß‚é")]
    PosXRotY = 34,

    [Tooltip("PositionY‚ÆRotationY‚ğ~‚ß‚é")]
    PosYRotY = 36,

    [Tooltip("PositionXY‚ÆRotationY‚ğ~‚ß‚é")]
    PosXYRotY = 38,

    [Tooltip("PositionZ‚ÆRotationY‚ğ~‚ß‚é")]
    PosZRotY = 40,

    [Tooltip("PositionXZ‚ÆRotationY‚ğ~‚ß‚é")]
    PosXZRotY = 42,

    [Tooltip("PositionYZ‚ÆRotationY‚ğ~‚ß‚é")]
    PosYZRotY = 44,

    [Tooltip("Position‘S‚Ä‚ÆRotationY‚ğ~‚ß‚é")]
    PosRotY = 46,

    [Tooltip("RotationXY‚ğ~‚ß‚é")]
    RotXY = 48,

    [Tooltip("PositionX‚ÆRotationXY‚ğ~‚ß‚é")]
    PosXRotXY = 50,

    [Tooltip("PositionY‚ÆRotationXY‚ğ~‚ß‚é")]
    PosYRotXY = 52,

    [Tooltip("PositionXY‚ÆRotationXY‚ğ~‚ß‚é")]
    PosXYRotXY = 54,

    [Tooltip("PositionZ‚ÆRotationXY‚ğ~‚ß‚é")]
    PosZRotXY = 56,

    [Tooltip("PositionXZ‚ÆRotationXY‚ğ~‚ß‚é")]
    PosXZRotXY = 58,

    [Tooltip("PositionYZ‚ÆRotationXY‚ğ~‚ß‚é")]
    PosYZRotXY = 60,

    [Tooltip("Position‘S‚Ä‚ÆRotationXY‚ğ~‚ß‚é")]
    PosRotXY = 62,

    [Tooltip("RotationZ‚ğ~‚ß‚é")]
    RotZ = 64,

    [Tooltip("PositionX‚ÆRotationZ‚ğ~‚ß‚é")]
    PosXRotZ = 66,

    [Tooltip("PositionY‚ÆRotationZ‚ğ~‚ß‚é")]
    PosYRotZ = 68,

    [Tooltip("PositionXY‚ÆRotationZ‚ğ~‚ß‚é")]
    PosXYRotZ = 70,

    [Tooltip("PositionZ‚ÆRotationZ‚ğ~‚ß‚é")]
    PosZRotZ = 72,

    [Tooltip("PositionXZ‚ÆRotationZ‚ğ~‚ß‚é")]
    PosXZRotZ = 74,

    [Tooltip("PositionYZ‚ÆRotationZ‚ğ~‚ß‚é")]
    PosYZRotZ = 76,

    [Tooltip("Position‘S‚Ä‚ÆRotationZ‚ğ~‚ß‚é")]
    PosRotZ = 78,

    [Tooltip("RotationX‚ÆZ‚ğ~‚ß‚é")]
    RotXZ = 80,

    [Tooltip("PositionX‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosXRotXZ = 82,

    [Tooltip("PositionY‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosYRotXZ = 84,

    [Tooltip("PositionXY‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosXYRotXZ = 86,

    [Tooltip("PositionZ‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosZRotXZ = 88,

    [Tooltip("PositionXZ‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosXZRotXZ = 90,

    [Tooltip("PositionYZ‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosYZRotXZ = 92,

    [Tooltip("Position‘S‚Ä‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosRotXZ = 94,

    [Tooltip("RotationY‚ÆZ‚ğ~‚ß‚é")]
    RotYZ = 96,

    [Tooltip("PositionX‚ÆRotationYZ‚ğ~‚ß‚é")]
    PosXRotYZ = 98,

    [Tooltip("PositionY‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosYRotYZ = 100,

    [Tooltip("PositionXY‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosXYRotYZ = 102,

    [Tooltip("PositionZ‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosZRotYZ = 104,

    [Tooltip("PositionXZ‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosXZRotYZ = 106,

    [Tooltip("PositionYZ‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosYZRotYZ = 108,

    [Tooltip("Position‘S‚Ä‚ÆRotationXZ‚ğ~‚ß‚é")]
    PosRotYZ = 110,

    [Tooltip("Rotation‘S‚Ä‚ğ~‚ß‚é")]
    Rot = 112,

    [Tooltip("PositionX‚ÆRotation‘S‚Ä‚ğ~‚ß‚é")]
    PosXRot = 114,

    [Tooltip("PositionY‚ÆRotation‘S‚Ä‚ğ~‚ß‚é")]
    PosYRot = 116,

    [Tooltip("PositionXY‚ÆRotation‘S‚Ä‚ğ~‚ß‚é")]
    PosXYRot = 118,

    [Tooltip("PositionZ‚ÆRotation‘S‚Ä‚ğ~‚ß‚é")]
    PosZRot = 120,

    [Tooltip("PositionXZ‚ÆRotation‘S‚Ä‚ğ~‚ß‚é")]
    PosXZRot = 122,

    [Tooltip("PositionYZ‚ÆRotation‘S‚Ä‚ğ~‚ß‚é")]
    PosYZRot = 124,

    [Tooltip("‘S‚Ä~‚ß‚é")]
    All = 126,
}
