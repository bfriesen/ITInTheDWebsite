using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITinTheDWebSite.Helpers;

namespace ITinTheDWebSite
{
    /// <summary>
    /// Summary description for GetFileImage
    /// </summary>
    public class GetFileImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["id"] != null)
            {
                int id = int.Parse(context.Request.QueryString["id"]);
                UserImage f = DatabaseHelper.GetImage(id);       // Retrieve File with this ID from the database
                context.Response.Clear();
                context.Response.AddHeader("Content-Length", f.ContentLength.ToString());
                context.Response.ContentType = f.ContentType;
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + f.FileName + ";");
                context.Response.OutputStream.Write(f.FileContent, 0, f.ContentLength);
                context.Response.End();

                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}