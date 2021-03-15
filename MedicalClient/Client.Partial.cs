using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalClient
{
    public partial class ParticipantClient
    {
        private string _token;

        public ParticipantClient(string token) : this()
        {
            _token = token;
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            request.Headers.Add("Authorization", $"Bearer {_token}");
        }

    }

    public partial class ResearcherClient
    {
        private string _token;

        public ResearcherClient(string token) : this()
        {
            _token = token;
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            request.Headers.Add("Authorization", $"Bearer {_token}");
        }

    }

    public partial class DataPointClient
    {
        private string _token;

        public DataPointClient(string token) : this()
        {
            _token = token;
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            request.Headers.Add("Authorization", $"Bearer {_token}");
        }

    }

}
