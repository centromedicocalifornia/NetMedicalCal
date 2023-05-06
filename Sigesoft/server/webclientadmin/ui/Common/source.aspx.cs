using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class source : PageBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string files = Request.QueryString["files"];

                if (String.IsNullOrEmpty(files))
                {
                    return;
                }

                if (!String.IsNullOrEmpty(files))
                {
                    string[] fileNames = files.Split(';');

                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        string fileName = fileNames[i].Trim();
                        if (String.IsNullOrEmpty(fileName))
                        {
                            continue;
                        }
						
						if (fileName.StartsWith("~"))
						{
							fileName = fileName.Substring(1);
						}

                        if (fileName.Contains("/mobile/?file="))
                        {
                            fileName = fileName.Replace("/mobile/?file=", "/mobile/");
                        }


                        int lastQuestionMaskPosition = fileName.IndexOf("?");
                        if (lastQuestionMaskPosition >= 0)
                        {
                            fileName = fileName.Substring(0, lastQuestionMaskPosition);
                        }

                        string shortFileName = GetShortFileName(fileName);
                        string iframeUrl = "./source_file.aspx?file=" + fileName;

                        FineUIPro.Tab tab = new FineUIPro.Tab();
                        tab.Title = shortFileName;
                        tab.EnableIFrame = true;
                        tab.IFrameUrl = iframeUrl;
                        tab.IconUrl = GetIconUrl(tab.IFrameUrl);
                        tab.TitleToolTip = fileName;
                        TabStrip1.Tabs.Add(tab);

                        if (fileName.ToLower().EndsWith(".aspx") 
                            || fileName.ToLower().EndsWith(".ascx") 
                            || fileName.ToLower().EndsWith(".master")
                            || fileName.ToLower().EndsWith(".ashx"))
                        {
                            tab = new FineUIPro.Tab();
                            tab.Title = shortFileName + ".cs";
                            tab.EnableIFrame = true;
                            tab.IFrameUrl = iframeUrl + ".cs";
                            tab.IconUrl = GetIconUrl(tab.IFrameUrl);
                            tab.TitleToolTip = fileName + ".cs";
                            TabStrip1 .Tabs.Add(tab);
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string GetIconUrl(string url)
        {
            string suffix = url.Substring(url.LastIndexOf('.') + 1);
            return "~/res/images/filetype/vs_" + suffix + ".png";
        }

        private string GetShortFileName(string fileName)
        {
            int index = fileName.LastIndexOf("/");

            if (index >= 0)
            {
                return fileName.Substring(index + 1);
            }

            return fileName;
        }
    }
}
