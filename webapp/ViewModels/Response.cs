﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace webapp.ViewModels
{
    public class Response
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public int MessageTimeOut { get; set; }
        public string RedirectTo { get; set; }
        public object Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Kind { get; set; }
        public string Id { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }
    }
}