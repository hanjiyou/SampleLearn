using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 两种特性
/// AssetsOnly特性对应的Obj，点击打开选择弹窗时，只有Project中的资源文件，不会出现Hierachy（场景）的资源
/// SceneObjectsOnly:点击需要序列化的资源字段时，在出现的弹窗中只有Hierachy中的资源文件，不会出现Project中的资源（public默认）
/// </summary>
public class AssetsOnly : MonoBehaviour
{
    public GameObject AllGO;
    
    [AssetsOnly]
    public List<GameObject> OnlyPrefabs;

    [AssetsOnly]
    public GameObject SomePrefab;

    [AssetsOnly]
    public Material MaterialAsset;

    [AssetsOnly]
    public MeshRenderer SomeMeshRendererOnPrefab;

    [SceneObjectsOnly]
    public List<GameObject> OnlySceneObjects;

    [SceneObjectsOnly]
    public GameObject SomeSceneObject;

    [SceneObjectsOnly]
    public MeshRenderer SomeMeshRenderer;
}
