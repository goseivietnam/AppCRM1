using AppCRM.Models;
using AppCRM.Services.Request;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace AppCRM.Services.Candidate
{
    public interface ICandidateExploreService
    {

    }

    class CandidateExploreService : ICandidateExploreService
    {
        private readonly IRequestService _requestService;

        public CandidateExploreService(IRequestService requestService)
        {
            _requestService = requestService;
        }
    }
}
