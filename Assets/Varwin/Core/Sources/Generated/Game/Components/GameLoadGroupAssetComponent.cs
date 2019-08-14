//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Varwin.ECS.Components.Events.LoadGroupAssetComponent loadGroupAsset { get { return (Varwin.ECS.Components.Events.LoadGroupAssetComponent)GetComponent(GameComponentsLookup.LoadGroupAsset); } }
    public bool hasLoadGroupAsset { get { return HasComponent(GameComponentsLookup.LoadGroupAsset); } }

    public void AddLoadGroupAsset(Varwin.Data.ServerData.ObjectDto newValue, int newIdPhoton) {
        var index = GameComponentsLookup.LoadGroupAsset;
        var component = CreateComponent<Varwin.ECS.Components.Events.LoadGroupAssetComponent>(index);
        component.Value = newValue;
        component.IdPhoton = newIdPhoton;
        AddComponent(index, component);
    }

    public void ReplaceLoadGroupAsset(Varwin.Data.ServerData.ObjectDto newValue, int newIdPhoton) {
        var index = GameComponentsLookup.LoadGroupAsset;
        var component = CreateComponent<Varwin.ECS.Components.Events.LoadGroupAssetComponent>(index);
        component.Value = newValue;
        component.IdPhoton = newIdPhoton;
        ReplaceComponent(index, component);
    }

    public void RemoveLoadGroupAsset() {
        RemoveComponent(GameComponentsLookup.LoadGroupAsset);
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

    static Entitas.IMatcher<GameEntity> _matcherLoadGroupAsset;

    public static Entitas.IMatcher<GameEntity> LoadGroupAsset {
        get {
            if (_matcherLoadGroupAsset == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LoadGroupAsset);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLoadGroupAsset = matcher;
            }

            return _matcherLoadGroupAsset;
        }
    }
}
