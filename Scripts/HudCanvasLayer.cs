using Godot;

public partial class HudCanvasLayer : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	[Signal]
	public delegate void ShowReadyMessageEventHandler();

    [Signal]
    public delegate void ShowGameOverMessageEventHandler();

	[Signal]
	public delegate void ScoreUpdateEventHandler(string newScore);


	public void ShowReadyMessageEmit() =>
		EmitSignal(SignalName.ShowReadyMessage);

	public void ShowGameOverEmit() =>
		EmitSignal(SignalName.ShowGameOverMessage);

	public void UpdateScoreEmit(int score) =>
		EmitSignal(SignalName.ScoreUpdate, score.ToString());

	public void OnStartButtonPressed() =>
		EmitSignal(SignalName.StartGame);
}
