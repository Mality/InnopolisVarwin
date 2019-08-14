//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Varwin.ECS.Components.Events.DestroyComponent destroy { get { return (Varwin.ECS.Components.Events.DestroyComponent)GetComponent(GameComponentsLookup.Destroy); } }
    public bool hasDestroy { get { return HasComponent(GameComponentsLookup.Destroy); } }

    public void AddDestroy(bool newValue) {
        var index = GameComponentsLookup.Destroy;
        var component = CreateComponent<Varwin.ECS.Components.Events.DestroyComponent>(index);
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDestroy(bool newValue) {
        var index = GameComponentsLookup.Destroy;
        var component = CreateComponent<Varwin.ECS.Components.Events.DestroyComponent>(index);
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDestroy() {
        RemoveComponent(GameComponentsLookup.Destroy);
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

    static Entitas.IMatcher<GameEntity> _matcherDestroy;

    public static Entitas.IMatcher<GameEntity> Destroy {
        get {
            if (_matcherDestroy == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Destroy);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDestroy = matcher;
            }

            return _matcherDestroy;
        }
    }
}
