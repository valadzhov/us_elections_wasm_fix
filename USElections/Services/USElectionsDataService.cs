using System.Net.Http.Json;
using System.Reactive.Subjects;
using USElections.Models.USElectionsData;
using USElections.State;

namespace USElections.USElectionsData
{
    public class USElectionsDataService: IUSElectionsDataService
    {
        private readonly HttpClient _http;
        private readonly IStateService _stateService;

        public USElectionsDataService(HttpClient http, IStateService stateService)
        {
            _http = http;
            _stateService = stateService;
        }

        private BehaviorSubject<VoteCountResult> electoralVotesDemocrat;
        public BehaviorSubject<VoteCountResult> ElectoralVotesDemocrat
        {
            get
            {
                if (electoralVotesDemocrat == null)
                {
                    electoralVotesDemocrat = new(null);
                    _stateService.CurrentlyChosenYear.Subscribe(async _ => electoralVotesDemocrat.OnNext(await GetVoteCountResult((int)_stateService.CurrentlyChosenYear.Value, "Democrat")));
                }
                return electoralVotesDemocrat;
            }
        }
        private BehaviorSubject<VoteCountResult> popularVotesRepublican;
        public BehaviorSubject<VoteCountResult> PopularVotesRepublican
        {
            get
            {
                if (popularVotesRepublican == null)
                {
                    popularVotesRepublican = new(null);
                    _stateService.CurrentlyChosenYear.Subscribe(async _ => popularVotesRepublican.OnNext(await GetVoteCountResult1((int)_stateService.CurrentlyChosenYear.Value, "Republican")));
                }
                return popularVotesRepublican;
            }
        }
        private BehaviorSubject<VoteCountResult> popularVotesDemocrat;
        public BehaviorSubject<VoteCountResult> PopularVotesDemocrat
        {
            get
            {
                if (popularVotesDemocrat == null)
                {
                    popularVotesDemocrat = new(null);
                    _stateService.CurrentlyChosenYear.Subscribe(async _ => popularVotesDemocrat.OnNext(await GetVoteCountResult1((int)_stateService.CurrentlyChosenYear.Value, "Democrat")));
                }
                return popularVotesDemocrat;
            }
        }
        private BehaviorSubject<Candidate> democratCandidate;
        public BehaviorSubject<Candidate> DemocratCandidate
        {
            get
            {
                if (democratCandidate == null)
                {
                    democratCandidate = new(null);
                    _stateService.CurrentlyChosenYear.Subscribe(async _ => democratCandidate.OnNext(await GetCandidate((int)_stateService.CurrentlyChosenYear.Value)));
                }
                return democratCandidate;
            }
        }
        private BehaviorSubject<VoteCountResult> electoralVotesRepublican;
        public BehaviorSubject<VoteCountResult> ElectoralVotesRepublican
        {
            get
            {
                if (electoralVotesRepublican == null)
                {
                    electoralVotesRepublican = new(null);
                    _stateService.CurrentlyChosenYear.Subscribe(async _ => electoralVotesRepublican.OnNext(await GetVoteCountResult((int)_stateService.CurrentlyChosenYear.Value, "Republican")));
                }
                return electoralVotesRepublican;
            }
        }
        private BehaviorSubject<Candidate> republicanCandidate;
        public BehaviorSubject<Candidate> RepublicanCandidate
        {
            get
            {
                if (republicanCandidate == null)
                {
                    republicanCandidate = new(null);
                    _stateService.CurrentlyChosenYear.Subscribe(async _ => republicanCandidate.OnNext(await GetCandidate1((int)_stateService.CurrentlyChosenYear.Value)));
                }
                return republicanCandidate;
            }
        }

        public async Task<List<YearModel>> GetYearModelList()
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://elections.appbuilder.dev/api/Election/years", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<YearModel>>().ConfigureAwait(false);
            }

            return new List<YearModel>();
        }

        public async Task<List<VoteResult>> GetVoteResultList(int? year)
        {
            if (year == null)
            {
                return new List<VoteResult>();
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://elections.appbuilder.dev/api/Election/electoral-votes/{year}", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<VoteResult>>().ConfigureAwait(false);
            }

            return new List<VoteResult>();
        }

        public async Task<List<VoteResult>> GetVoteResultList1(int? year)
        {
            if (year == null)
            {
                return new List<VoteResult>();
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://elections.appbuilder.dev/api/Election/popular-votes/{year}", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<VoteResult>>().ConfigureAwait(false);
            }

            return new List<VoteResult>();
        }

        public async Task<List<CandidateVoteResult>> GetCandidateVoteResultList(int? year)
        {
            if (year == null)
            {
                return new List<CandidateVoteResult>();
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://elections.appbuilder.dev/api/Election/votes/{year}/by-candidate", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CandidateVoteResult>>().ConfigureAwait(false);
            }

            return new List<CandidateVoteResult>();
        }

        public async Task<List<StateVoteResult>> GetStateVoteResultList(int? year)
        {
            if (year == null)
            {
                return new List<StateVoteResult>();
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://elections.appbuilder.dev/api/Election/popular-votes/{year}/by-state", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<StateVoteResult>>().ConfigureAwait(false);
            }

            return new List<StateVoteResult>();
        }

        public async Task<VoteCountResult> GetVoteCountResult(int? year, string? party)
        {
            if (year == null || party == null)
            {
                return null;
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://elections.appbuilder.dev/api/Election/electoral-votes/{year}/{party}", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VoteCountResult>().ConfigureAwait(false);
            }

            return null;
        }

        public async Task<VoteCountResult> GetVoteCountResult1(int? year, string? party)
        {
            if (year == null || party == null)
            {
                return null;
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://elections.appbuilder.dev/api/Election/popular-votes/{year}/{party}", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VoteCountResult>().ConfigureAwait(false);
            }

            return null;
        }

        public async Task<Candidate> GetCandidate(int? year)
        {
            if (year == null)
            {
                return null;
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://elections.appbuilder.dev/api/Election/democratic-candidate/{year}", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Candidate>().ConfigureAwait(false);
            }

            return null;
        }

        public async Task<Candidate> GetCandidate1(int? year)
        {
            if (year == null)
            {
                return null;
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://elections.appbuilder.dev/api/Election/republican-candidate/{year}", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Candidate>().ConfigureAwait(false);
            }

            return null;
        }
    }
}
