using Godot;

public partial class HudCanvasLayer : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	[Signal]
	public delegate void ShowReadingMessageEventHandler();

    [Signal]
    public delegate void ShowGamingOverMessageEventHandler();

	[Signal]
	public delegate void ScoreUpdateingEventHandler(string newScore);


	public void ShowReadyMessage() =>
		EmitSignal(SignalName.ShowReadingMessage);

	public void ShowGameOver() =>
		EmitSignal(SignalName.ShowGamingOverMessage);

	public void UpdateScore(int score) =>
		EmitSignal(SignalName.ScoreUpdateing, score.ToString());

	public void OnStartButtonPressed() =>
		EmitSignal(SignalName.StartGame);
}
