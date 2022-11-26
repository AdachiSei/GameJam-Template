using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Rigitbody�̊g�����\�b�h�������Ă���Script
/// </summary>
public static class RigitbodyExtensions
{
    #region Method

    public static Rigidbody ChangeConstraints(this Rigidbody rb,Freeze type)
    {
        var constraints = (RigidbodyConstraints)type;
        rb.constraints = constraints;
        return rb;
    }

    #endregion
}
public enum Freeze
{ 
    [Tooltip("�S����")]
    None = 0,

    [Tooltip("PositionX���~�߂�")]
    PosX = 2,

    [Tooltip("PositionY���~�߂�")]
    PosY = 4,

    [Tooltip("PositionX��Y���~�߂�")]
    PosXY = 6,

    [Tooltip("PositionZ���~�߂�")]
    PosZ = 8,

    [Tooltip("PositionX��Z���~�߂�")]
    PosXZ = 10,

    [Tooltip("PositionY��Z���~�߂�")]
    PosYZ = 12,

    [Tooltip("Position�S�Ă��~�߂�")]
    Pos = 14,

    [Tooltip("RotationX���~�߂�")]
    RotX = 16,

    [Tooltip("PositionX��RotationX���~�߂�")]
    PosXRotX = 18,

    [Tooltip("PositionY��RotationX���~�߂�")]
    PosYRotX = 20,

    [Tooltip("PositionXY��RotationX���~�߂�")]
    PosXYRotX = 22,

    [Tooltip("PositionZ��RotationX���~�߂�")]
    PosZRotX = 24,

    [Tooltip("PositionXZ��RotationX���~�߂�")]
    PosXZRotX = 26,

    [Tooltip("PositionYZ��RotationX���~�߂�")]
    PosYZRotX = 28,

    [Tooltip("Position�S�Ă�RotationX���~�߂�")]
    PosRotX = 30,

    [Tooltip("RotationY���~�߂�")]
    RotY = 32,

    [Tooltip("PositionX��RotationY���~�߂�")]
    PosXRotY = 34,

    [Tooltip("PositionY��RotationY���~�߂�")]
    PosYRotY = 36,

    [Tooltip("PositionXY��RotationY���~�߂�")]
    PosXYRotY = 38,

    [Tooltip("PositionZ��RotationY���~�߂�")]
    PosZRotY = 40,

    [Tooltip("PositionXZ��RotationY���~�߂�")]
    PosXZRotY = 42,

    [Tooltip("PositionYZ��RotationY���~�߂�")]
    PosYZRotY = 44,

    [Tooltip("Position�S�Ă�RotationY���~�߂�")]
    PosRotY = 46,

    [Tooltip("RotationXY���~�߂�")]
    RotXY = 48,

    [Tooltip("PositionX��RotationXY���~�߂�")]
    PosXRotXY = 50,

    [Tooltip("PositionY��RotationXY���~�߂�")]
    PosYRotXY = 52,

    [Tooltip("PositionXY��RotationXY���~�߂�")]
    PosXYRotXY = 54,

    [Tooltip("PositionZ��RotationXY���~�߂�")]
    PosZRotXY = 56,

    [Tooltip("PositionXZ��RotationXY���~�߂�")]
    PosXZRotXY = 58,

    [Tooltip("PositionYZ��RotationXY���~�߂�")]
    PosYZRotXY = 60,

    [Tooltip("Position�S�Ă�RotationXY���~�߂�")]
    PosRotXY = 62,

    [Tooltip("RotationZ���~�߂�")]
    RotZ = 64,

    [Tooltip("PositionX��RotationZ���~�߂�")]
    PosXRotZ = 66,

    [Tooltip("PositionY��RotationZ���~�߂�")]
    PosYRotZ = 68,

    [Tooltip("PositionXY��RotationZ���~�߂�")]
    PosXYRotZ = 70,

    [Tooltip("PositionZ��RotationZ���~�߂�")]
    PosZRotZ = 72,

    [Tooltip("PositionXZ��RotationZ���~�߂�")]
    PosXZRotZ = 74,

    [Tooltip("PositionYZ��RotationZ���~�߂�")]
    PosYZRotZ = 76,

    [Tooltip("Position�S�Ă�RotationZ���~�߂�")]
    PosRotZ = 78,

    [Tooltip("RotationX��Z���~�߂�")]
    RotXZ = 80,

    [Tooltip("PositionX��RotationXZ���~�߂�")]
    PosXRotXZ = 82,

    [Tooltip("PositionY��RotationXZ���~�߂�")]
    PosYRotXZ = 84,

    [Tooltip("PositionXY��RotationXZ���~�߂�")]
    PosXYRotXZ = 86,

    [Tooltip("PositionZ��RotationXZ���~�߂�")]
    PosZRotXZ = 88,

    [Tooltip("PositionXZ��RotationXZ���~�߂�")]
    PosXZRotXZ = 90,

    [Tooltip("PositionYZ��RotationXZ���~�߂�")]
    PosYZRotXZ = 92,

    [Tooltip("Position�S�Ă�RotationXZ���~�߂�")]
    PosRotXZ = 94,

    [Tooltip("RotationY��Z���~�߂�")]
    RotYZ = 96,

    [Tooltip("PositionX��RotationYZ���~�߂�")]
    PosXRotYZ = 98,

    [Tooltip("PositionY��RotationXZ���~�߂�")]
    PosYRotYZ = 100,

    [Tooltip("PositionXY��RotationXZ���~�߂�")]
    PosXYRotYZ = 102,

    [Tooltip("PositionZ��RotationXZ���~�߂�")]
    PosZRotYZ = 104,

    [Tooltip("PositionXZ��RotationXZ���~�߂�")]
    PosXZRotYZ = 106,

    [Tooltip("PositionYZ��RotationXZ���~�߂�")]
    PosYZRotYZ = 108,

    [Tooltip("Position�S�Ă�RotationXZ���~�߂�")]
    PosRotYZ = 110,

    [Tooltip("Rotation�S�Ă��~�߂�")]
    Rot = 112,

    [Tooltip("PositionX��Rotation�S�Ă��~�߂�")]
    PosXRot = 114,

    [Tooltip("PositionY��Rotation�S�Ă��~�߂�")]
    PosYRot = 116,

    [Tooltip("PositionXY��Rotation�S�Ă��~�߂�")]
    PosXYRot = 118,

    [Tooltip("PositionZ��Rotation�S�Ă��~�߂�")]
    PosZRot = 120,

    [Tooltip("PositionXZ��Rotation�S�Ă��~�߂�")]
    PosXZRot = 122,

    [Tooltip("PositionYZ��Rotation�S�Ă��~�߂�")]
    PosYZRot = 124,

    [Tooltip("�S�Ď~�߂�")]
    All = 126,
}
