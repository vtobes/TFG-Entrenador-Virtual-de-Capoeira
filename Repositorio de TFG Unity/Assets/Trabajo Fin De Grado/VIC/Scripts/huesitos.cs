using System.Collections;
using System.Collections.Generic;
// DrawBones.cs
using UnityEngine;

public class huesitos : MonoBehaviour
{
    private SkinnedMeshRenderer m_Renderer;
    // Variable to hold all them bones. It will initialize the same size as initialRotations.
    protected Transform[] bones;
    private LineRenderer[] lines;

    private GameObject[] cubebones;

    public LineRenderer skeletonLine;
    public LineRenderer skeletonLine_red;
    public LineRenderer debugLine;

    public GameObject cubeJointGreen;
    public GameObject cubeJointRed;


    public bool _Show_bones = false;
    public bool Show_bones
    {
        set { _Show_bones = value; }
    }

    
    public void Paint_Bone_red(int joint)

    {
        int i = GetBoneIndexByJoint((KinectInterop.JointType)joint, true);

        lines[i].GetComponent<LineRenderer>().colorGradient = skeletonLine_red.GetComponent<LineRenderer>().colorGradient;
        cubebones[i].GetComponent<Renderer>().material = cubeJointRed.GetComponent<Renderer>().material;
    }

    public void Reset_Color_bones()
    {
        if (bones != null)
        {
            for (int boneIndex = 1; boneIndex < bones.Length; boneIndex++)
            {
                if (!boneIndex2MecanimMap.ContainsKey(boneIndex))
                    continue;

                lines[boneIndex].GetComponent<LineRenderer>().colorGradient = skeletonLine.GetComponent<LineRenderer>().colorGradient;
                cubebones[boneIndex].GetComponent<Renderer>().material = cubeJointGreen.GetComponent<Renderer>().material;

            }
        }
    }

    /// <summary>
    /// Gets the bone index by joint type.
    /// </summary>
    /// <returns>The bone index.</returns>
    /// <param name="joint">Joint type</param>
    /// <param name="bMirrored">If set to <c>true</c> gets the mirrored joint index.</param>
    public int GetBoneIndexByJoint(KinectInterop.JointType joint, bool bMirrored)
    {
        int boneIndex = -1;

        if (jointMap2boneIndex.ContainsKey(joint))
        {
            boneIndex = !bMirrored ? jointMap2boneIndex[joint] : mirrorJointMap2boneIndex[joint];
        }

        return boneIndex;
    }


    void Start()
    {

        // inits the bones array
        bones = new Transform[31];

        cubebones = new GameObject[bones.Length];
        lines = new LineRenderer[bones.Length];
        // get bone transforms from the animator component
        Animator animatorComponent = GetComponent<Animator>();

        for (int boneIndex = 1; boneIndex < bones.Length; boneIndex++)
        {
            if (!boneIndex2MecanimMap.ContainsKey(boneIndex))
                continue;

            bones[boneIndex] = animatorComponent ? animatorComponent.GetBoneTransform(boneIndex2MecanimMap[boneIndex]) : null;
            lines[boneIndex] = Instantiate(skeletonLine) as LineRenderer;
            lines[boneIndex].transform.parent = transform;

            cubebones[boneIndex] = Instantiate(cubeJointGreen) as GameObject;
            cubebones[boneIndex].transform.parent = transform;



            //lines[boneIndex].GetComponent<LineRenderer>().colorGradient = skeletonLine_red.GetComponent<LineRenderer>().colorGradient;

        }
    }

    void LateUpdate()
    {


      
            for (int boneIndex = 1; boneIndex < bones.Length; boneIndex++)
            {
                if (!boneIndex2MecanimMap.ContainsKey(boneIndex))
                    continue;



            if (!_Show_bones)
            {
                lines[boneIndex].gameObject.SetActive(false);
                cubebones[boneIndex].gameObject.SetActive(false);
            }
            else
            {
                lines[boneIndex].gameObject.SetActive(true);
                cubebones[boneIndex].gameObject.SetActive(true);
                Vector3 posJoint2 = bones[boneIndex].position;

                Vector3 dirFromParent = bones[boneIndex].parent.position;
                //dirFromParent.z = !mirroredMovement ? -dirFromParent.z : dirFromParent.z;
                //Vector3 posParent = posJoint2 - dirFromParent;

                //lines[i].SetVertexCount(2);
                lines[boneIndex].SetPosition(0, dirFromParent);
                lines[boneIndex].SetPosition(1, posJoint2);

                cubebones[boneIndex].transform.position = posJoint2;
            }


            }
        
    }

   
    protected static readonly Dictionary<KinectInterop.JointType, int> jointMap2boneIndex = new Dictionary<KinectInterop.JointType, int>
    {
        {KinectInterop.JointType.SpineBase, 0},
        {KinectInterop.JointType.SpineMid, 1},
        {KinectInterop.JointType.SpineShoulder, 2},
        {KinectInterop.JointType.Neck, 3},
        {KinectInterop.JointType.Head, 4},

        {KinectInterop.JointType.ShoulderLeft, 5},
        {KinectInterop.JointType.ElbowLeft, 6},
        {KinectInterop.JointType.WristLeft, 7},
        {KinectInterop.JointType.HandLeft, 8},

        {KinectInterop.JointType.HandTipLeft, 9},
        {KinectInterop.JointType.ThumbLeft, 10},

        {KinectInterop.JointType.ShoulderRight, 11},
        {KinectInterop.JointType.ElbowRight, 12},
        {KinectInterop.JointType.WristRight, 13},
        {KinectInterop.JointType.HandRight, 14},

        {KinectInterop.JointType.HandTipRight, 15},
        {KinectInterop.JointType.ThumbRight, 16},

        {KinectInterop.JointType.HipLeft, 17},
        {KinectInterop.JointType.KneeLeft, 18},
        {KinectInterop.JointType.AnkleLeft, 19},
        {KinectInterop.JointType.FootLeft, 20},

        {KinectInterop.JointType.HipRight, 21},
        {KinectInterop.JointType.KneeRight, 22},
        {KinectInterop.JointType.AnkleRight, 23},
        {KinectInterop.JointType.FootRight, 24},
    };

    protected static readonly Dictionary<int, HumanBodyBones> boneIndex2MecanimMap = new Dictionary<int, HumanBodyBones>
    {
        {0, HumanBodyBones.Hips},
        {1, HumanBodyBones.Spine},
        {2, HumanBodyBones.Chest},
        {3, HumanBodyBones.Neck},
        {4, HumanBodyBones.Head},

        {5, HumanBodyBones.LeftUpperArm},
        {6, HumanBodyBones.LeftLowerArm},
        {7, HumanBodyBones.LeftHand},
//		{8, HumanBodyBones.LeftIndexProximal},
//		{9, HumanBodyBones.LeftIndexIntermediate},
//		{10, HumanBodyBones.LeftThumbProximal},
		
		{11, HumanBodyBones.RightUpperArm},
        {12, HumanBodyBones.RightLowerArm},
        {13, HumanBodyBones.RightHand},
//		{14, HumanBodyBones.RightIndexProximal},
//		{15, HumanBodyBones.RightIndexIntermediate},
//		{16, HumanBodyBones.RightThumbProximal},
		
		{17, HumanBodyBones.LeftUpperLeg},
        {18, HumanBodyBones.LeftLowerLeg},
        {19, HumanBodyBones.LeftFoot},
        {20, HumanBodyBones.LeftToes},

        {21, HumanBodyBones.RightUpperLeg},
        {22, HumanBodyBones.RightLowerLeg},
        {23, HumanBodyBones.RightFoot},
        {24, HumanBodyBones.RightToes},

        {25, HumanBodyBones.LeftShoulder},
        {26, HumanBodyBones.RightShoulder},
        {27, HumanBodyBones.LeftIndexProximal},
        {28, HumanBodyBones.RightIndexProximal},
        {29, HumanBodyBones.LeftThumbProximal},
        {30, HumanBodyBones.RightThumbProximal},
    };

    protected static readonly Dictionary<KinectInterop.JointType, int> mirrorJointMap2boneIndex = new Dictionary<KinectInterop.JointType, int>
    {
        {KinectInterop.JointType.SpineBase, 0},
        {KinectInterop.JointType.SpineMid, 1},  
        {KinectInterop.JointType.SpineShoulder, 2}, //neck
        {KinectInterop.JointType.Neck, 3},
        {KinectInterop.JointType.Head, 4},

        {KinectInterop.JointType.ShoulderRight,25},
        {KinectInterop.JointType.ElbowRight,11},
        {KinectInterop.JointType.WristRight,12},
        {KinectInterop.JointType.HandRight,13},

        {KinectInterop.JointType.HandTipRight,28},
        {KinectInterop.JointType.ThumbRight,30},

        {KinectInterop.JointType.ShoulderLeft,26},
        {KinectInterop.JointType.ElbowLeft,5},
        {KinectInterop.JointType.WristLeft,6},
        {KinectInterop.JointType.HandLeft,7},

        {KinectInterop.JointType.HandTipLeft,27},
        {KinectInterop.JointType.ThumbLeft,29},

        {KinectInterop.JointType.HipRight,21},
        {KinectInterop.JointType.KneeRight,22},
        {KinectInterop.JointType.AnkleRight,23},
        {KinectInterop.JointType.FootRight,24},

        {KinectInterop.JointType.HipLeft,17},
        {KinectInterop.JointType.KneeLeft,18},
        {KinectInterop.JointType.AnkleLeft,19},
        {KinectInterop.JointType.FootLeft,20},
    };

}

