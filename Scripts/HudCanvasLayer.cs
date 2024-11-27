using Godot;

public partial class HudCanvasLayer : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	[Signal]
	public delegate void ShowReadingMessageEventHandler();

    [Signal]
    public delegate void ShowGamingOverMessageEventHandler();



    private Label _scoreLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("ScoreLabel");
    }


	public void ShowReadyMessage() =>
		EmitSignal(SignalName.ShowReadingMessage);


	public void ShowGameOver() =>
		EmitSignal(SignalName.ShowGamingOverMessage);

    public void UpdateScore(int score) =>
        _scoreLabel.Text = score.ToString();


	public void OnStartButtonPressed() =>
		EmitSignal(SignalName.StartGame);
}
