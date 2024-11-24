using Godot;
using System;
public partial class MainNode : Node
{

	[Export]
	public PackedScene Enemy { get; set; }

    [Signal]
    public delegate void ScoreUpdateEventHandler(int newScore);

    [Signal]
    public delegate void GameEndEventHandler();



    private int _score;

    private int Score 
    {
        get => _score;
        set 
        {
            _score = value;
            EmitSignal(SignalName.ScoreUpdate, _score);
        }
    }


    private Timer _startTimer;
    private Timer _enemyTimer;
    private Timer _scoreTimer;

    private PlayerArea2d _playerArea2d;
    private Marker2D _startPlayerPositionMarker2D;

    private PathFollow2D _enemySpawnPathFollow2D;

    private HudCanvasLayer _hudCanvasLayer;

    public override void _Ready()
    {
        _startTimer = GetNode<Timer>("TimersNode/StartTimer");
        _enemyTimer = GetNode<Timer>("TimersNode/EnemyTimer");
        _scoreTimer = GetNode<Timer>("TimersNode/ScoreTimer");

        _playerArea2d = GetNode<PlayerArea2d>("PlayerArea2D");
        _startPlayerPositionMarker2D = GetNode<Marker2D>("StartPlayerPositionMarker2D");

        _enemySpawnPathFollow2D = GetNode<PathFollow2D>("EnemyPath2D/EnemySpawnPathFollow2D");

        _hudCanvasLayer = GetNode<HudCanvasLayer>("HUDCanvasLayer");
    }


    public void GameOver()
	{
        _enemyTimer.Stop();
        _scoreTimer.Stop();
        
        EmitSignal(SignalName.GameEnd);
    }

    public void StartGame()
    {
        Score = 0;

        _playerArea2d.Start(_startPlayerPositionMarker2D.Position);

        _startTimer.Start();

        _hudCanvasLayer.ShowMessage("Get Ready!");

        GetTree().CallGroup("Enemies", Node.MethodName.QueueFree);
    }

    public void OnStartTimerTimeout()
    {
        _enemyTimer.Start();
        _scoreTimer.Start();
    }

    public void OnScoreTimerTimeout() =>
        Score++;

    public void OnEnemyTimerTimeout()
    {
        EnemyRigidBody2D enemyRigidBody2D = Enemy.Instantiate<EnemyRigidBody2D>();

        _enemySpawnPathFollow2D.ProgressRatio = GD.Randf();

        float rotation = _enemySpawnPathFollow2D.Rotation + Mathf.Pi / 2 + (float)GD.RandRange(-Mathf.Pi / 3, Mathf.Pi / 3);
        Vector2 velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0).Rotated(rotation);

        enemyRigidBody2D.Position = _enemySpawnPathFollow2D.Position;
        enemyRigidBody2D.Rotation = rotation;
        enemyRigidBody2D.LinearVelocity = velocity;

        AddChild(enemyRigidBody2D);
    }

}
