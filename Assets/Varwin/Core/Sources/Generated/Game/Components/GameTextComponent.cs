//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Varwin.ECS.Components.UnityBehaviour.TextComponent text { get { return (Varwin.ECS.Components.UnityBehaviour.TextComponent)GetComponent(GameComponentsLookup.Text); } }
    public bool hasText { get { return HasComponent(GameComponentsLookup.Text); } }

    public void AddText(UnityEngine.UI.Text newValue) {
        var index = GameComponentsLookup.Text;
        var component = CreateComponent<Varwin.ECS.Components.UnityBehaviour.TextComponent>(index);
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceText(UnityEngine.UI.Text newValue) {
        var index = GameComponentsLookup.Text;
        var component = CreateComponent<Varwin.ECS.Components.UnityBehaviour.TextComponent>(index);
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveText() {
        RemoveComponent(GameComponentsLookup.Text);
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

    static Entitas.IMatcher<GameEntity> _matcherText;

    public static Entitas.IMatcher<GameEntity> Text {
        get {
            if (_matcherText == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Text);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherText = matcher;
            }

            return _matcherText;
        }
    }
}
