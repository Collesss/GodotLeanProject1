using Godot;
using System.Threading.Tasks;

public partial class HudCanvasLayer : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

    private Label _scoreLabel;
    private Label _messageLabel;
	private Timer _messageTimer;
	private Button _startButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("ScoreLabel");
        _messageLabel = GetNode<Label>("MessageLabel");
		_messageTimer = GetNode<Timer>("MessageTimer");
		_startButton = GetNode<Button>("StartButton");
    }


	public void ShowMessage(string text) 
	{
		_messageLabel.Text = text;
		_messageLabel.Show();
		_messageTimer.Start();
    }

	public async void ShowGameOver()
	{
		ShowMessage("Game Over");

		await ToSignal(_messageTimer, Timer.SignalName.Timeout);

		_messageLabel.Text = "Dodge the Creeps!";
		_messageLabel.Show();

		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        //await Task.Delay(1000);
        _startButton.Show();
    }

	public void UpdateScore(int score) =>
        _scoreLabel.Text = score.ToString();


	public void OnMessageTimerTimeout() =>
		_messageLabel.Hide();

	public void OnStartButtonPressed()
	{
		_startButton.Hide();
		EmitSignal(SignalName.StartGame);
	}
}
