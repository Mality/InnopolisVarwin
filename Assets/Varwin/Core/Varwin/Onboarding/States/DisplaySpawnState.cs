using SmartLocalization;
using Varwin.UI;

namespace Varwin.Onboarding
{
    public class DisplaySpawnState : State
    {
        public DisplaySpawnState(OnboardingStateMachine tutorialStateMachine) : base(tutorialStateMachine)
        {
        }

        public override void OnUpdate()
        {

            if (GameStateData.GetWrapperCollection() == null)
            {
                return;
            }

            var group = Contexts.sharedInstance.game.GetGroup(GameMatcher.AllOf(GameMatcher.Wrapper, GameMatcher.IdObject));

            foreach (var gameEntity in group.GetEntities())
            {
                if (gameEntity.idObject.Value == StateMachine.SpawningObjectId)
                {
                    StateMachine.DisplayGameObject = gameEntity.gameObject.Value;
                    StateMachine.ChangeState(new MenuOpeningState(StateMachine, MenuOpeningState.TargetObject.Lamp));
                    return;
                }
            }

            if (ProjectData.SelectedObjectIdToSpawn == 0)
            {
                StateMachine.SpawningObjectId = 0;
                StateMachine.ChangeState(new MenuOpeningState(StateMachine, MenuOpeningState.TargetObject.Display));

                return;
            }
        }

        public override void OnEnter()
        {
            UIMenu.Instance.OnboardingBlock = true;
            PopupWindowManager.ClosePopup();
            string spawnText = LanguageManager.Instance.GetTextValue("TUTORIAL_TOOLTIP_OBJECT_SPAWN");
            spawnText = string.Format(spawnText, LanguageManager.Instance.GetTextValue("DISPLAY"));
            string cancelText = LanguageManager.Instance.GetTextValue("TUTORIAL_TOOLTIP_CANCEL_SPAWN");
            TooltipManager.ShowControllerTooltip(spawnText, ControllerTooltipManager.TooltipControllers.Right, ControllerTooltipManager.TooltipButtons.Trigger);
            TooltipManager.ShowControllerTooltip(cancelText, ControllerTooltipManager.TooltipControllers.Right, ControllerTooltipManager.TooltipButtons.Grip);
        }

        public override void OnExit()
        {
            UIMenu.Instance.OnboardingBlock = false;
            TooltipManager.HideControllerTooltip(ControllerTooltipManager.TooltipControllers.Right, ControllerTooltipManager.TooltipButtons.Trigger);
            TooltipManager.HideControllerTooltip(ControllerTooltipManager.TooltipControllers.Right, ControllerTooltipManager.TooltipButtons.Grip);
        }
    }
}
