using Godot;
using System;

public partial class ScoreHandler : Node2D
{
	[Export] Button ResetScore;

	[ExportCategory("ScoreDetectors")]
	[Export] Area2D P1ScoreDetector;
	[Export] Area2D P2ScoreDetector;

	[ExportCategory("ScoreLabels")]
	[Export] Label P1ScoreLabel;
	[Export] Label P2ScoreLabel;

    public override void _Ready()
    {
        // Connect Signals
		P1ScoreDetector.BodyEntered += P1ScoreDetector_BodyEntered;
		P2ScoreDetector.BodyEntered += P2ScoreDetector_BodyEntered;

		ResetScore.Pressed += ResetScore_Pressed;
    }

	private void P1ScoreDetector_BodyEntered(Node2D otherBody)
	{
		if (otherBody.IsInGroup("Ball"))
		{
			Global.Instance.P2Score += 1;
			P2ScoreLabel.Text = Global.Instance.P2Score.ToString();

			Global.Instance.Reset();
		}
	}

	private void P2ScoreDetector_BodyEntered(Node2D otherBody)
	{
		if (otherBody.IsInGroup("Ball"))
		{
			Global.Instance.P1Score += 1;
			P1ScoreLabel.Text = Global.Instance.P1Score.ToString();

			Global.Instance.Reset();
		}
	}

	private void ResetScore_Pressed()
	{
		Global.Instance.Reset();

		Global.Instance.P1Score = 0;
		Global.Instance.P2Score = 0;

		P2ScoreLabel.Text = Global.Instance.P2Score.ToString();
		P1ScoreLabel.Text = Global.Instance.P1Score.ToString();
	}
}
