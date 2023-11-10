using FluentAssertions;
using Veloci.Logic.Domain;
using Veloci.Logic.Services;

namespace Veloci.Tests;

public class RaceResultDeltaAnalyzerTests
{
    [Fact]
    public void one_player_made_a_progress ()
    {
        var a = new TrackResults()
        {
            Times = new List<TrackTime>
            {
                new(2, "PlayerOne", 50),
                new(3, "PlayerTwo", 60),
                new(1, "PlayerThree", 40)
            }
        };
        
        var b = new TrackResults()
        {
            Times = new List<TrackTime>
            {
                new(1, "PlayerOne", 35),
                new(3, "PlayerTwo", 60),
                new(2, "PlayerThree", 40)
            }
        };

        var deltaAnalyzer = new RaceResultDeltaAnalyzer();
        var deltas = deltaAnalyzer.CompareResults(a, b);

        deltas.Should().HaveCount(1);
        var delta = deltas[0];
        delta.PlayerName.Should().Be("PlayerOne");
        delta.TrackTime.Should().Be(35);
        delta.Rank.Should().Be(1);
        delta.RankOld.Should().Be(2);
    }
}