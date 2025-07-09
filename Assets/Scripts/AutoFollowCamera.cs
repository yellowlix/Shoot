using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTran;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        playerTran = GameObject.FindWithTag(TagConst.Player).transform;
        if (playerTran != null)
        {
            virtualCamera.Follow = playerTran;
        }
    }
}
