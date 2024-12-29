using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    public Animator animator;
    [SerializeField] private ContainerCounter cointainerCounter;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cointainerCounter.OnPlayerGrabbedObject += CointainerCounter_OnPlayerGraggedObejct;
    }

    private void CointainerCounter_OnPlayerGraggedObejct(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
