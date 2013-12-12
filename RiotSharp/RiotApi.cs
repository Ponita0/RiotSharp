﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RiotSharp
{
    public class RiotApi
    {
        private const String RootUrl = "/api/lol/{0}/v1.1/summoner";
        private const String NameUrl = "/by-name/{0}";
        private const String IdUrl = "/{0}";
        private const String NamesUrl = "/{0}/name";

        private readonly Requester requester;

        internal JsonSerializerSettings JsonSerializerSettings { get; set; }
        
        static RiotApi()
        {
            Requester.RootDomain = "prod.api.pvp.net";
            Requester.IsProdApi = false;
        }

        public RiotApi(String apiKey)
        {
            JsonSerializerSettings = new JsonSerializerSettings();
            JsonSerializerSettings.CheckAdditionalContent = false;
            JsonSerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            requester = Requester.Instance;
            Requester.ApiKey = apiKey;
        }

        public Summoner GetSummoner(Region region, int summonerId)
        {
            var request = requester
                .CreateRequest(String.Format(RootUrl, region.ToString()) + String.Format(IdUrl, summonerId));
            var response = (HttpWebResponse)request.GetResponse();
            var result = requester.GetResponseString(response.GetResponseStream());
            var json = JObject.Parse(result);
            return new Summoner(this, json, requester);
        }

        //public Summoner GetSummoner(Region region, String summonerName)
        //{

        //}
    }
}
