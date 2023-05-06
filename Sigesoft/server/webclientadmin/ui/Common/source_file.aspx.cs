using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class source_file : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string file = Request.QueryString["file"].ToLower();

                if (file.StartsWith("http://") || file.StartsWith("https://"))
                {
                    desc.Text = String.Format("<br/><br/><a href=\"{0}\" target=\"_blank\">Open in new window</a>", file);
                    return;
                }

                if (!UnderRootPath(file))
                {
                    return;
                }

                string basePath = GetBasePath(file);
                List<string> disallowPaths = new List<string> { "bin", "obj", "upload", "Properties" };
                if (disallowPaths.Contains(basePath))
                {
                    return;
                }


                string fileType = GetFileType(file);
                List<string> allowFileTypes = new List<string> { "aspx", "ascx", "master", "ashx", "cs", "xml", "css", "js" };
                if (!allowFileTypes.Contains(fileType))
                {
                    return;
                }

                string content = File.ReadAllText(Server.MapPath(file));
                desc.Text = "<pre class=\"prettyprint\">" + HttpUtility.HtmlEncode(content) + "</pre>"; // linenums

            }
        }

        private bool UnderRootPath(string fileName)
        {
            string filePath = Server.MapPath(fileName);
            string rootPath = Server.MapPath("~/");

            return filePath.StartsWith(rootPath);
        }

        private string GetBasePath(string fileName)
        {
            string filePath = Server.MapPath(fileName);
            string rootPath = Server.MapPath("~/");

            string basePath = filePath.Substring(rootPath.Length);
            int slashIndex = basePath.IndexOf("\\");
            if (slashIndex >= 0)
            {
                basePath = basePath.Substring(0, slashIndex);
            }

            return basePath;
        }

        private string GetFileType(string fileName)
        {
            string fileType = String.Empty;

            int lastDotIndex = fileName.ToLower().LastIndexOf(".");
            if (lastDotIndex >= 0)
            {
                fileType = fileName.Substring(lastDotIndex + 1);
            }

            return fileType;
        }
    }
}
