using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

using FineUIPro;
using AspNet = System.Web.UI.WebControls;


namespace Sigesoft.Server.WebClientAdmin.UI
{
    public class PageBase : System.Web.UI.Page
    {
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            var pm = PageManager.Instance;
            if (pm != null)
            {
                HttpCookie themeCookie = Request.Cookies["Theme"];
                if (themeCookie != null)
                {
                    string themeValue = themeCookie.Value;

                    if (IsSystemTheme(themeValue))
                    {
                        pm.CustomTheme = String.Empty;
                        pm.Theme = (Theme)Enum.Parse(typeof(Theme), themeValue, true);
                    }
                    else
                    {
                        pm.CustomTheme = themeValue;
                    }
                }

                HttpCookie langCookie = Request.Cookies["Language"];
                if (langCookie != null)
                {
                    string langValue = langCookie.Value;
                    try
                    {
                        pm.Language = (Language)Enum.Parse(typeof(Language), langValue, true);
                    }
                    catch (Exception)
                    {
                        pm.CustomLanguage = langValue;
                    }
                }


                HttpCookie modeCookie = Request.Cookies["MenuMode"];
                if (modeCookie != null)
                {
                    string modeValue = modeCookie.Value;
                    try
                    {
                        pm.DisplayMode = (DisplayMode)Enum.Parse(typeof(DisplayMode), modeValue, true);
                    }
                    catch (Exception)
                    {
                        pm.DisplayMode = DisplayMode.Normal;
                    }
                }


                HttpCookie loadingCookie = Request.Cookies["Loading"];
                if (loadingCookie != null)
                {
                    int loadingNumber = Convert.ToInt32(loadingCookie.Value);
                    pm.LoadingImageNumber = loadingNumber;
                }

                if (SaveFStateToServer)
                {
                    pm.EnableFStatePersistence = true;
                    pm.LoadFStateFromPersistenceMedium = LoadFStateFromPersistenceMedium_Cache;
                    pm.SaveFStateToPersistenceMedium = SaveFStateToPersistenceMedium_Cache;
                }


                System.Web.UI.HtmlControls.HtmlGenericControl linkCtrl = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
                linkCtrl.Attributes["rel"] = "stylesheet";
                linkCtrl.Attributes["type"] = "text/css";
                linkCtrl.Attributes["href"] = ResolveClientUrl("~/res/css/common.css?v" + GlobalConfig.ProductVersion);
                Header.Controls.AddAt(GetHeadStyleCSSIndex(), linkCtrl);


                var commonJSPath = String.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", ResolveClientUrl("~/res/js/common.js?v" + GlobalConfig.ProductVersion));
                PageContext.RegisterStartupScript("FineUIPro_Examples_common_js", commonJSPath, false);



                Form.Attributes["autocomplete"] = "off";
            }

            base.OnInit(e);
        }

        private bool IsSystemTheme(string themeName)
        {
            themeName = themeName.ToLower();
            string[] themes = Enum.GetNames(typeof(Theme));
            foreach (string theme in themes)
            {
                if (theme.ToLower() == themeName)
                {
                    return true;
                }
            }
            return false;
        }

        private int GetHeadStyleCSSIndex()
        {
            var theIndex = 0;
            for (var i = 0; i < Header.Controls.Count; i++)
            {
                var ctrl = Header.Controls[i];
                if (ctrl is LiteralControl)
                {
                    if ((ctrl as LiteralControl).Text.Trim().ToLower().StartsWith("<style"))
                    {
                        theIndex = i;
                        break;
                    }
                }
                else if (ctrl is System.Web.UI.HtmlControls.HtmlLink)
                {
                    var theCtrl = ctrl as System.Web.UI.HtmlControls.HtmlLink;
                    if (theCtrl.TagName == "link")
                    {
                        var typeAttr = theCtrl.Attributes["type"];
                        var relAttr = theCtrl.Attributes["rel"];
                        if ((typeAttr != null && typeAttr.ToLower() == "text/css")
                            || (relAttr != null && relAttr.ToLower() == "stylesheet"))
                        {
                            theIndex = i;
                            break;
                        }
                    }
                }
            }
            return theIndex;
        }
        /// <summary>
        /// Whether to save FState to the server
        /// </summary>
        protected virtual bool SaveFStateToServer
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Save FState to server file

        private static readonly string FSTATE_FILE_KEY = "__FSTATE_KEY";
        private static readonly string FSTATE_FILE_BASE_PATH = "~/App_Data/FState/";

        private JObject LoadFStateFromPersistenceMedium()
        {
            string filePath = GetFStateFilePath();
            string fileContent;
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
            {
                fileContent = sr.ReadToEnd();
            }
            return JObject.Parse(fileContent);
        }

        private void SaveFStateToPersistenceMedium(JObject fstate)
        {
            string filePath = GenerateFStateFilePath();
            using (StreamWriter streamW = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                streamW.Write(fstate.ToString(Formatting.None));
            }
        }

        private string GenerateFStateFilePath()
        {
            string filePath = String.Empty;
            string fileName = String.Empty;

            string cacheKey = Page.Request.Form[FSTATE_FILE_KEY];
            if (String.IsNullOrEmpty(cacheKey))
            {
                DateTime now = DateTime.Now;
                string folderName = now.ToString("yyyyMMddHH");
                fileName = String.Format("{0}_{1}",
                    HttpContext.Current.Session.SessionID,
                    now.Ticks.ToString());

                string folderPath = Page.Server.MapPath(Path.Combine(FSTATE_FILE_BASE_PATH, folderName));
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                filePath = folderPath + "/" + fileName + ".config";
            }
            else
            {
                fileName = cacheKey;
                filePath = GetFStateFilePath();
            }

            if (!PageManager.Instance.IsFineUIAjaxPostBack)
            {
                PageContext.RegisterStartupScript(String.Format("F.setHidden('{0}','{1}');", FSTATE_FILE_KEY, fileName));
            }

            return filePath;
        }

        private string GetFStateFilePath()
        {
            string fileName = Request.Form[FSTATE_FILE_KEY];
            string[] fileNames = fileName.Split('_');
            string folderName = new DateTime(Convert.ToInt64(fileNames[1])).ToString("yyyyMMddHH");

            return Page.Server.MapPath(Path.Combine(FSTATE_FILE_BASE_PATH, folderName)) + "/" + fileName + ".config";
        }

        #endregion

        #region Save FState to server cache

        private static readonly string FSTATE_CACHE_KEY = "__FSTATE_KEY";

        private JObject LoadFStateFromPersistenceMedium_Cache()
        {
            string cacheKey = Page.Request.Form[FSTATE_CACHE_KEY];

            return HttpRuntime.Cache[cacheKey] as JObject;
        }

        private void SaveFStateToPersistenceMedium_Cache(JObject fstate)
        {
            string cacheKey = Page.Request.Form[FSTATE_CACHE_KEY];
            if (String.IsNullOrEmpty(cacheKey))
            {
                cacheKey = String.Format("{0}_{1}",
                    HttpContext.Current.Session.SessionID,
                    DateTime.Now.Ticks.ToString());
            }

            if (!PageManager.Instance.IsFineUIAjaxPostBack)
            {
                PageContext.RegisterStartupScript(String.Format("F.setHidden('{0}','{1}');", FSTATE_CACHE_KEY, cacheKey));
            }

            HttpRuntime.Cache.Insert(cacheKey, fstate, null,
                DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout),
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.Default, null);
        }

        #endregion

        #region Upload file type check

        protected readonly static List<string> VALID_FILE_TYPES = new List<string> { "jpg", "bmp", "gif", "jpeg", "png" };

        protected static bool ValidateFileType(string fileName)
        {
            string fileType = String.Empty;
            int lastDotIndex = fileName.LastIndexOf(".");
            if (lastDotIndex >= 0)
            {
                fileType = fileName.Substring(lastDotIndex + 1).ToLower();
            }

            if (VALID_FILE_TYPES.Contains(fileType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region Province, City and Country

        protected readonly static JArray SHENG_JSON = JArray.Parse("[\"Beijing\",\"Shanghai\",\"Anhui\"]");
        protected readonly static JObject SHI_JSON = JObject.Parse("{\"Beijing\":[\"Beijing City\"],\"Shanghai\":[\"Shanghai City\"],\"Anhui\":[\"Hefei City\",\"Huangshan City\"]}");
        protected readonly static JObject XIAN_JSON = JObject.Parse("{\"Beijing City\":[\"Dongcheng District\",\"Xicheng District\",\"Chongwen District\",\"Xuanwu District\",\"Chaoyang District\",\"Fengtai District\",\"Shijingshan District\",\"Haidian District\",\"Mentougou District\",\"Fangshan District\",\"Tongzhou District\",\"Shunyi District\",\"Changping District\",\"Daxing District\",\"Huairou District\",\"Pinggu District\",\"Miyun County\",\"Yanqing County\"],\"Shanghai City\":[\"Huangpu District\",\"Luwan District\",\"Xuhui District\",\"Changning District\",\"Jing'an District\",\"Putuo District\",\"Zhabei District\",\"Hongkou District\",\"Yangpu District\",\"Baoshan District\",\"Minhang District\",\"Jiading District\",\"Songjiang District\",\"Jinshan District\",\"Qingpu District\",\"Fengxian District\",\"Pudong New District\",\"Chongming County\"],\"Hefei City\":[\"Shushan District\",\"Luyang District\",\"Yaohai District\",\"Baohe District\",\"Changfeng County\",\"Feidong County\",\"Feixi County\"],\"Huangshan City\":[\"Tunxi District\",\"Huangshan District\",\"Huizhou District\",\"Xiuning County\",\"Shexian County\",\"Qimen County\",\"Yixian County\"]}");

        #endregion

        #region Grid related


        /// <summary>
        /// Which rows are selected
        /// </summary>
        /// <param name="grid">Grid object</param>
        /// <returns>Information for selected rows</returns>
        protected string HowManyRowsAreSelected(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            int selectedCount = grid.SelectedRowIDArray.Length;
            if (selectedCount > 0)
            {
                sb.AppendFormat("<p><strong>Number of selected rows: {0}</strong></p>", selectedCount);
                sb.Append("<table class=\"result\">");

                sb.Append("<tr><th>Row number</th>");
                foreach (string datakey in grid.DataKeyNames)
                {
                    sb.AppendFormat("<th>{0}</th>", datakey);
                }
                sb.Append("</tr>");


                for (int i = 0; i < selectedCount; i++)
                {
                    string rowId = grid.SelectedRowIDArray[i];
                    GridRow row = grid.FindRow(rowId);

                    sb.Append("<tr>");
                    int rownumber = row.RowIndex + 1;
                    if (grid.AllowPaging && grid.IsDatabasePaging)
                    {
                        rownumber += grid.PageIndex * grid.PageSize;
                    }
                    sb.AppendFormat("<td>{0}</td>", rownumber);

                    object[] dataKeys = grid.DataKeys[row.RowIndex];
                    for (int j = 0; j < dataKeys.Length; j++)
                    {
                        sb.AppendFormat("<td>{0}</td>", dataKeys[j]);
                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Append("<strong>No rows selected!</strong>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get the literal value of the Gender, called in the ASPX
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        protected string GetGender(object gender)
        {
            if (Convert.ToInt32(gender) == 1)
            {
                return "Male";
            }
            else
            {
                return "Female";
            }
        }

        #endregion

        #region ViewState - Compress



        #endregion

        #region Utility function

        /// <summary>
        /// Get the parameters of the postback
        /// </summary>
        /// <returns></returns>
        public string GetRequestEventArgument()
        {
            return Request.Form["__EVENTARGUMENT"];
        }

        /// <summary>
        /// Show notify dialog
        /// </summary>
        /// <param name="message"></param>
        public virtual void ShowNotify(string message)
        {
            ShowNotify(message, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Show notify dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageIcon"></param>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon)
        {
            Notify n = new Notify();
            n.Target = Target.Top;
            n.Message = message;
            n.MessageBoxIcon = messageIcon;
            n.PositionX = Position.Center;
            n.PositionY = Position.Top;
            n.DisplayMilliseconds = 3000;
            n.ShowHeader = false;


            n.Show();
        }

        #endregion

        #region Grid data url

        private JArray _cachedSelectedRowData;

        /// <summary>
        /// Get the selected row data (from client)
        /// </summary>
        /// <param name="paramRowId"></param>
        /// <returns></returns>
        protected JObject GetDataUrlSelectedRowValue(string paramRowId)
        {
            if (_cachedSelectedRowData == null)
            {
                _cachedSelectedRowData = JArray.Parse(Request.Form["DataUrl_Grid_SelectedRowData"]);
            }

            foreach (JObject row in _cachedSelectedRowData)
            {
                string rowId = row.Value<string>("id");

                if (rowId == paramRowId)
                {
                    return row.Value<JObject>("values");
                }
            }

            return null;
        }

        #endregion

        #region DataView - RowFilter

        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }



        #endregion
    }
}