using FluentAssertions;
using Veloci.Logic.Services;

namespace Veloci.Tests;

public class MessageParserTests
{
    private const string StartCompetitionMessage = "🏁 Трек дня 06.11.2023 - 07.11.2023:\nEmpty Scene Day - TBS EU Spec Series 8 Race 7\n\nЛаскаво просимо на щоденний онлайн-турнір з дрон перегонів ім. Віктора Дзензеля!\n\nУмови за посиланням: http://sim.droner.com.ua\n\nЗапрошуй друзів та покращуй свої результати разом із ними!\n\n#velocibottotd\n\nЛідерборд:\nhttps://www.velocidrone.com/leaderboard/16/814/All\n\nШукати трек на YouTube:\nhttp://www.youtube.com/results?search_query=Empty+Scene+Day+TBS+EU+Spec+Series+8+Race+7&oq=Empty+Scene+Day+TBS+EU+Spec+Series+8+Race+7";
    
    [Fact]
    public void DetectStartCompetition_normal_message()
    {
        var start = MessageParser.IsStartCompetition(StartCompetitionMessage);
        start.Should().BeTrue();
    }
    
    [Fact]
    public void DetectStartCompetition_random_message()
    {
        var message = "hello world";
        var start = MessageParser.IsStartCompetition(message);
        start.Should().BeFalse();
    }

    [Fact]
    public void GetTrackId()
    {
        var id = MessageParser.GetTrackId(StartCompetitionMessage);
        id.Should().Be(814);
        
        var anotherMessage = "🏁 Трек дня 06.11.2023 - 07.11.2023:\nEmpty Scene Day - TBS EU Spec Series 8 Race 7\n\nЛаскаво просимо на щоденний онлайн-турнір з дрон перегонів ім. Віктора Дзензеля!\n\nУмови за посиланням: http://sim.droner.com.ua\n\nЗапрошуй друзів та покращуй свої результати разом із ними!\n\n#velocibottotd\n\nЛідерборд:\nhttps://www.velocidrone.com/leaderboard/16/14/All\n\nШукати трек на YouTube:\nhttp://www.youtube.com/results?search_query=Empty+Scene+Day+TBS+EU+Spec+Series+8+Race+7&oq=Empty+Scene+Day+TBS+EU+Spec+Series+8+Race+7";
        var anotherId = MessageParser.GetTrackId(anotherMessage);
        anotherId.Should().Be(14);
    }

    [Fact]
    public void GetTrackName()
    {
        var mapAndTrack = MessageParser.GetTrackName(StartCompetitionMessage);

        mapAndTrack.map.Should().Be("Empty Scene Day");
        mapAndTrack.track.Should().Be("TBS EU Spec Series 8 Race 7");
    }
}