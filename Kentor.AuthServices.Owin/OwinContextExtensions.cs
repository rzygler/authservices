﻿using Kentor.AuthServices.WebSso;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kentor.AuthServices.Configuration;

namespace Kentor.AuthServices.Owin
{
    static class OwinContextExtensions
    {
        public async static Task<HttpRequestData> ToHttpRequestData(this IOwinContext context, IOptions options)
        {
            if(context == null)
            {
                return null;
            }

            IFormCollection formData = null;
            if(context.Request.Body != null)
            {
                formData = await context.Request.ReadFormAsync();
            }

            var applicationRootPath = context.Request.PathBase.Value;
            if(string.IsNullOrEmpty(applicationRootPath))
            {
                applicationRootPath = "/";
            }
            return new HttpRequestData(
                context.Request.Method,
                options?.SPOptions.PublicOrigin?? context.Request.Uri,
                applicationRootPath,
                formData);
        }
    }
}
