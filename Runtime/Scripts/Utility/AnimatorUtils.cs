using UnityEngine;

public static class AnimatorUtils
{
}

public static class AnimationTable
{
    public static int[,] HashTable = new int[,]
    {
        //Animator ID left foot ik, Animator ID right foot ik
        { Animator.StringToHash("0_FootIKWeightLeft"), Animator.StringToHash("0_FootIKWeightRight") }, // 0 - Stand Rifle Lowered
        { Animator.StringToHash("1_FootIKWeightLeft"), Animator.StringToHash("1_FootIKWeightRight") }, // 1 - Jog Forward
        { Animator.StringToHash("2_FootIKWeightLeft"), Animator.StringToHash("2_FootIKWeightRight") }, // 2 - Jog Forward Right
        { Animator.StringToHash("3_FootIKWeightLeft"), Animator.StringToHash("3_FootIKWeightRight") }, // 3 - Jog Strafe Right
        { Animator.StringToHash("4_FootIKWeightLeft"), Animator.StringToHash("4_FootIKWeightRight") }, // 4 - Jog Backward Right
        { Animator.StringToHash("5_FootIKWeightLeft"), Animator.StringToHash("5_FootIKWeightRight") }, // 5 - Jog Backward
        { Animator.StringToHash("6_FootIKWeightLeft"), Animator.StringToHash("6_FootIKWeightRight") }, // 6 - Jog Backward Left
        { Animator.StringToHash("7_FootIKWeightLeft"), Animator.StringToHash("7_FootIKWeightRight") }, // 7 - Jog Strafe Left
        { Animator.StringToHash("8_FootIKWeightLeft"), Animator.StringToHash("8_FootIKWeightRight") } // 8 - Jog Forward Left
    };
    public static int[,] ConvertedHashTable = new int[,]
    {
        { -721940723, -1984135381 },
        { 1001044460, 1583284083 },
        { -785619826, -49776166 },
        { 1048498799, 720042370 },
        { -548349941, 1624535753 },
        { 805465834, -1221135727 },
        { -628837496, 341865528 },
        { 903218537, -1014827936 },
        { -1012293375, -2145557842 },
    };
    public static string PrintConvertedHashTable()
    {
        string returnString = "{\n";
        for (int i = 0; i < HashTable.GetLength(0); i++)
        {
            returnString += "{ " + HashTable[i, 0] + ", " + HashTable[i, 1] + " }";
            if (i < HashTable.Length)
            {
                returnString += ",";
            }
            returnString += "\n";
        }
        returnString += "};";

        return returnString;
    }
}