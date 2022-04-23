using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using RankingTracker.Services.RankingTrackServices.Queries;
using RankingTracker.Services.GoogleRankingService;
using AutoMoq;
using RankingTracker.Model.Domain;
using System.Threading;
using FluentAssertions;
using Contract;

namespace RankingTracker.UnitTests.RankingTrackServices
{
    public  class GetRankingQueryHandlerTests
    {
        private AutoMoqer _mocker;
        private GetRankingQueryHandler _target { get; set; }
        public GetRankingQueryHandlerTests()
        {
            _mocker = new AutoMoqer();
            _target = _mocker.Create<GetRankingQueryHandler>();
        }

        [Fact]
        public async Task Should_return_response_correctly()
        {
            _mocker.GetMock<IGoogleRankingService>()
                .Setup(t => t.GetRankingSearchResult(It.IsAny<string>()))
                .ReturnsAsync(GenerateRankingHistory());

            var request = new GetRankingQuery() 
            { 
                SearchText = "conveyancing software", 
                SearchUrl = "www.smokeball.com" 
            };
            var result = await _target.Handle(request, new CancellationToken());
            var expected = new RankingTrackResponse
            {
                positions = new List<int> { 1, 3, 6 }
            };
            result.Should().BeEquivalentTo(expected);
        }


        // this just show I know how to do [Theory]
        [Theory]
        [InlineData("www.facebook.com", 2)]
        [InlineData("www.apple.com", 4)]
        [InlineData("www.amazon.com", 5)]
        public async Task Should_return_url_response_correctly(string searchUrl, int expectedRank)
        {
            _mocker.GetMock<IGoogleRankingService>()
               .Setup(t => t.GetRankingSearchResult(It.IsAny<string>()))
               .ReturnsAsync(GenerateRankingHistory());

            var request = new GetRankingQuery()
            {
                SearchText = "conveyancing software",
                SearchUrl = searchUrl
            };
            var result = await _target.Handle(request, new CancellationToken());
            var expected = new RankingTrackResponse
            {
                positions = new List<int> { expectedRank }
            };
            result.Should().BeEquivalentTo(expected);
        }

        private RankingSearchHistory GenerateRankingHistory()
        {
            return new RankingSearchHistory
            {
                SearchDateTimeOffset = DateTimeOffset.Now,
                SearchKey = "conveyancing software",
                Rankings = new List<Ranking>
                {
                    new Ranking{ Id = 1, Rank = 1, Key = "www.smokeball.com"},
                    new Ranking{ Id = 2, Rank = 2, Key = "www.facebook.com"},
                    new Ranking{ Id = 3, Rank = 3, Key = "www.smokeball.com"},
                    new Ranking{ Id = 4, Rank = 4, Key = "www.apple.com"},
                    new Ranking{ Id = 5, Rank = 5, Key = "www.amazon.com"},
                    new Ranking{ Id = 6, Rank = 6, Key = "www.smokeball.com"},
                }
            };
        }
    }
}
