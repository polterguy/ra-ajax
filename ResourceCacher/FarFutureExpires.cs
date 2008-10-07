/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web;
using System.Drawing;
using System.IO;

namespace ResourceCacher
{
    // Helper class for caching resources in the browser cache.
    // Should significantly improve "second time" downloads for
    // CSS and images...
    public class FarFutureExpires : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            string resource = context.Server.MapPath(context.Request.FilePath);
            context.Response.Cache.SetExpires(DateTime.Now.AddYears(3));
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetValidUntilExpires(false);
            switch (resource.Substring(resource.LastIndexOf(".") + 1))
            {
                case "jpg":
                case "jpeg":
                    context.Response.ContentType = "image/jpeg";
                    break;
                case "gif":
                    context.Response.ContentType = "image/gif";
                    break;
                case "css":
                    context.Response.ContentType = "text/css";
                    break;
                case "png":
                    context.Response.ContentType = "application/octet-stream";
                    break;
            }
            context.Response.WriteFile(resource);
            context.Response.Flush();
        }
    }
}
