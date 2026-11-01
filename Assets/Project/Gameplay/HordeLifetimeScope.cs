using Gameplay;
using VContainer;
using VContainer.Unity;

public sealed class HordeLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<RunState>();
        builder.RegisterComponentInHierarchy<EnemySpawner>();
        builder.RegisterComponentInHierarchy<InputManager>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.RegisterComponentInHierarchy<EnemyObjectPool>();
        builder.RegisterComponentInHierarchy<AmmoObjectPool>();
    }
}