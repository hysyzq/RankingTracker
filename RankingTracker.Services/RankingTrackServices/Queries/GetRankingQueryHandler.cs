using Contract;
using MediatR;
using Microsoft.Extensions.Logging;
using RankingTracker.Services.GoogleRankingService;

namespace RankingTracker.Services.RankingTrackServices.Queries
{
    public class GetRankingQuery : RankingTrackRequest, IRequest<RankingTrackResponse> { }

    public class GetRankingQueryHandler : IRequestHandler<GetRankingQuery, RankingTrackResponse>
    {
        private readonly IGoogleRankingService _googleRankingService;
        private readonly ILogger<GetRankingQueryHandler> _logger;

        public GetRankingQueryHandler(IGoogleRankingService googleRankingService, ILogger<GetRankingQueryHandler> logger)
        {
            _googleRankingService = googleRankingService;
            _logger = logger;
        }

        public async Task<RankingTrackResponse> Handle(GetRankingQuery request, CancellationToken cancellationToken)
        {
            var googelResults = await _googleRankingService.GetRankingSearchResult(request.SearchText);

            var findTargetedUrl = googelResults.Rankings
                                        .Where(t=>t.Key == request.SearchUrl)
                                        .Select(t=>t.Rank).ToList();

            return new RankingTrackResponse
            {
                positions = findTargetedUrl
            };
        }
    }
}
