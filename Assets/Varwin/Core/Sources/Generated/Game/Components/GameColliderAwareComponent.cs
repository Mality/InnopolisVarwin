//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Varwin.ECS.Components.ColliderAwareComponent colliderAware { get { return (Varwin.ECS.Components.ColliderAwareComponent)GetComponent(GameComponentsLookup.ColliderAware); } }
    public bool hasColliderAware { get { return HasComponent(GameComponentsLookup.ColliderAware); } }

    public void AddColliderAware(Varwin.Public.IColliderAware newValue) {
        var index = GameComponentsLookup.ColliderAware;
        var component = CreateComponent<Varwin.ECS.Components.ColliderAwareComponent>(index);
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceColliderAware(Varwin.Public.IColliderAware newValue) {
        var index = GameComponentsLookup.ColliderAware;
        var component = CreateComponent<Varwin.ECS.Components.ColliderAwareComponent>(index);
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveColliderAware() {
        RemoveComponent(GameComponentsLookup.ColliderAware);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherColliderAware;

    public static Entitas.IMatcher<GameEntity> ColliderAware {
        get {
            if (_matcherColliderAware == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ColliderAware);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherColliderAware = matcher;
            }

            return _matcherColliderAware;
        }
    }
}
