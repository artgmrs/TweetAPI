using RestSharp;
using System.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace TweetAPI.Infra.Config
{
    public class ResponseHandler
    {
        public static void HandleResponse(IRestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                string msg = $@"
                    URI = {response.ResponseUri}
                    StatusCode = {response.StatusCode}
                    Http Error = {response.StatusCode}
                    Response = {response.Content}           
                ";

                throw new WebException(msg);
            }
        }
    }
}
