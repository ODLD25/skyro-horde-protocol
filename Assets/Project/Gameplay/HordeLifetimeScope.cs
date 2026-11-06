using System;
using Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class HordeLifetimeScope : LifetimeScope
{
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private WeaponController weaponController;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<RunState>();
        builder.RegisterComponentInHierarchy<EnemyPool>();
        builder.RegisterComponentInHierarchy<AmmoPool>();
        builder.RegisterComponentInHierarchy<InputManager>();
        builder.RegisterComponentInHierarchy<EnemySpawner>();
        builder.RegisterComponentInHierarchy<EnemyFactory>();

        builder.RegisterComponent(playerMovement);
        builder.RegisterComponent(weaponController);
    }
}
