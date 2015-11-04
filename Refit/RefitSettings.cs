﻿using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Refit
{
    public class RefitSettings
    {
        public RefitSettings()
        {
            UrlParameterFormatter = new DefaultUrlParameterFormatter();
            UrlPathSegmentTransformer = new DelimiterEncodingPathSegmentTransformer();
        }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }
        public IUrlParameterFormatter UrlParameterFormatter { get; set; }
        public IUrlPathSegmentTransformer UrlPathSegmentTransformer { get; set; }
        public Func<Task<string>> AuthorizationHeaderValueGetter { get; set; }
        public Func<HttpMessageHandler> HttpMessageHandlerFactory { get; set; }
    }

    public interface IUrlParameterFormatter
    {
        string Format(object value, ParameterInfo parameterInfo);
    }

    public class DefaultUrlParameterFormatter : IUrlParameterFormatter
    {
        public virtual string Format(object parameterValue, ParameterInfo parameterInfo)
        {
            return parameterValue != null ? parameterValue.ToString() : null;
        }
    }

    public interface IUrlPathSegmentTransformer
    {
        string Transform(string value);
    }

    public class PassthroughPathSegmentTransformer : IUrlPathSegmentTransformer
    {
        public string Transform(string value) { return value; }
    }

    public class DelimiterEncodingPathSegmentTransformer : IUrlPathSegmentTransformer
    {
        public string Transform(string value) { return value?.Replace("/", "%2F"); }
    }
}
