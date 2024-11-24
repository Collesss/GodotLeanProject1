using Godot;

public partial class EnemyRigidBody2D : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] animNames = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(animNames[GD.RandRange(0, animNames.Length - 1)]);
    }

	public void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
