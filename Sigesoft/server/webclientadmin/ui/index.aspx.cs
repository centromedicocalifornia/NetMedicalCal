using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FineUIPro;

namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class index : PageBase      
    {
        #region EnableFStatePersistence

        /// <summary>
        /// Homepage does not have a postback operation, so there is no need to save FState on the server-side
        /// </summary>
        protected override bool SaveFStateToServer
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Page_Init

        private string _menuType = "tree";
        private string _searchText = "";

        private int _examplesCount = 0;

        
        private string _mainTabs = "multi";

        #region Page_Init

        
        protected void Page_Init(object sender, EventArgs e)
        {
            string themeStr = Request.QueryString["theme"];
            string menuStr = Request.QueryString["menu"];
            if (!String.IsNullOrEmpty(themeStr) || !String.IsNullOrEmpty(menuStr))
            {
                if (!String.IsNullOrEmpty(themeStr))
                {
                    if (themeStr == "bootstrap1")
                    {
                        themeStr = "bootstrap_pure";
                    }
                    HttpCookie cookie = new HttpCookie("Theme", themeStr);
                    cookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                }

                if (!String.IsNullOrEmpty(menuStr))
                {
                    HttpCookie cookie = new HttpCookie("MenuStyle", menuStr);
                    cookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                }

                PageContext.Redirect("~/default.aspx");
                return;
            }



            HttpCookie menuCookie = Request.Cookies["MenuStyle"];
            if (menuCookie != null)
            {
                _menuType = menuCookie.Value;
            }

            if (_menuType == "accordion")
            {
                _menuType = "tree";
            }


            HttpCookie searchText = Request.Cookies["SearchText"];
            if (searchText != null)
            {
                _searchText = HttpUtility.UrlDecode(searchText.Value);
            }


            // 从Cookie中读取 - 是否单标签页
            HttpCookie mainTabs = Request.Cookies["MainTabs"];
            if (mainTabs != null)
            {
                _mainTabs = mainTabs.Value;
            }


            InitTreeMenu();

        }

        #endregion


        #region InitTreeMenu

        private Tree InitTreeMenu()
        {
            treeMenu.MiniModePopWidth = System.Web.UI.WebControls.Unit.Pixel(260);
            

            if (_menuType == "tree")
            {
                treeMenu.HideHScrollbar = true;
                treeMenu.HideVScrollbar = true;
                treeMenu.ExpanderToRight = true;
                treeMenu.HeaderStyle = true;
                treeMenu.AllHeaderStyle = true;
                
            }

            XmlDocument doc = XmlDataSource1.GetXmlDocument();
            ResolveXmlDocument(doc);

            treeMenu.NodeDataBound += treeMenu_NodeDataBound;
            treeMenu.PreNodeDataBound += treeMenu_PreNodeDataBound;
            treeMenu.DataSource = doc;
            treeMenu.DataBind();

            return treeMenu;
        }

        #endregion

        #region ResolveXmlDocument

        private void ResolveXmlDocument(XmlDocument doc)
        {
            ResolveXmlDocument(doc, doc.DocumentElement.ChildNodes);
        }

        private int ResolveXmlDocument(XmlDocument doc, XmlNodeList nodes)
        {
            int nodeVisibleCount = 0;

            foreach (XmlNode node in nodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    XmlAttribute removedAttr;

                    bool isLeaf = node.ChildNodes.Count == 0;


                    if (isLeaf)
                    {
                        if (!String.IsNullOrEmpty(_searchText))
                        {
                            XmlAttribute textAttr = node.Attributes["Text"];
                            if (textAttr != null)
                            {
                                if (!textAttr.Value.Contains(_searchText) && isLeaf)
                                {
                                    removedAttr = doc.CreateAttribute("Removed");
                                    removedAttr.Value = "true";

                                    node.Attributes.Append(removedAttr);
                                }
                            }
                        }
                    }

                    if (!isLeaf)
                    {
                        int childVisibleCount = ResolveXmlDocument(doc, node.ChildNodes);

                        if (childVisibleCount == 0)
                        {
                            removedAttr = doc.CreateAttribute("Removed");
                            removedAttr.Value = "true";

                            node.Attributes.Append(removedAttr);
                        }
                    }


                    removedAttr = node.Attributes["Removed"];
                    if (removedAttr == null)
                    {
                        nodeVisibleCount++;
                    }
                }
            }

            return nodeVisibleCount;
        }

        #endregion

        #region treeMenu_NodeDataBound treeMenu_PreNodeDataBound
        /// <summary>
        /// Bind event of tree node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_NodeDataBound(object sender, FineUIPro.TreeNodeEventArgs e)
        {
            bool isLeaf = e.XmlNode.ChildNodes.Count == 0;


            if (isLeaf)
            {
                e.Node.ToolTip = e.Node.Text;
            }

            if (!String.IsNullOrEmpty(_searchText))
            {
                e.Node.Expanded = true;
            }
        }

        /// <summary>
        /// Pre-bind event of tree node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_PreNodeDataBound(object sender, FineUIPro.TreePreNodeEventArgs e)
        {
            bool isLeaf = e.XmlNode.ChildNodes.Count == 0;

            XmlAttribute removedAttr = e.XmlNode.Attributes["Removed"];
            if (removedAttr != null)
            {
                e.Cancelled = true;
            }

            if (isLeaf && !e.Cancelled)
            {
                _examplesCount++;
            }
        }

        #endregion


        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitSearchBox();
                InitMenuStyleButton();
                InitMenuDisplayModeButton();
                InitLangMenuButton();
				InitMenuRegionButton();

                menuTextCopyright.Text = "<div class=\"copyright\">" +
                    "<div class=\"version\"><a target=\"_blank\" href=\"http://fineui.us/\"><img src=\"./res/images/logo/logo_small.png\" alt=\"logo\"/></a>" +
                    "<br/><span>FineUI WebForms " + GlobalConfig.ProductVersion + "</span></div>" +
                    "</div>";

                hfExamplesCount.Text = _examplesCount.ToString();


                // Prevent client-side caching
                linkIndexCSS.Href = "~/res/css/index.css?v" + GlobalConfig.ProductVersion;
                linkMobileViewCss.Href = "~/res/css/mobileview.css?v" + GlobalConfig.ProductVersion;

                litScriptIndex.Text = String.Format("<script src=\"{0}\"></script>", ResolveClientUrl("~/res/js/index.js?v" + GlobalConfig.ProductVersion));


                // 单标签页
                if (_mainTabs == "single")
                {
                    mainTabStrip.ShowTabHeader = false;
                }
            }
        }


        private void InitSearchBox()
        {
            if (!String.IsNullOrEmpty(_searchText))
            {
                ttbxSearch.Text = _searchText;
                ttbxSearch.ShowTrigger1 = true;
                ttbxSearch.Width = 200;
                ttbxSearch.CssClass = "searchbox expanded";
            }
        }





        private void InitMenuStyleButton()
        {
            string menuStyle = "tree";

            HttpCookie menuStyleCookie = Request.Cookies["MenuStyle"];
            if (menuStyleCookie != null)
            {
                menuStyle = menuStyleCookie.Value;
            }

            SetSelectedMenuItem(MenuStyle, menuStyle);
        }

        private void InitMenuDisplayModeButton()
        {
            string displayMode = "normal";

            HttpCookie displayModeCookie = Request.Cookies["DisplayMode"];
            if (displayModeCookie != null)
            {
                displayMode = displayModeCookie.Value;
            }

            SetSelectedMenuItem(MenuDisplayMode, displayMode);
        }


        private void InitLangMenuButton()
        {
            string language = "en";

            HttpCookie languageCookie = Request.Cookies["Language"];
            if (languageCookie != null)
            {
                language = languageCookie.Value;
            }

            SetSelectedMenuItem(MenuLang, language);
        }

        private void InitMenuRegionButton()
        {
            string mainTabs = "multi";

            HttpCookie mainTabsCookie = Request.Cookies["MainTabs"];
            if (mainTabsCookie != null)
            {
                mainTabs = mainTabsCookie.Value;
            }

            SetSelectedMenuItem(MenuMainTabs, mainTabs);
        }

        private void SetSelectedMenuItem(MenuButton menuButton, string selectedDataTag)
        {
            foreach (FineUIPro.MenuItem item in menuButton.Menu.Items)
            {
                MenuCheckBox checkBox = (item as MenuCheckBox);
                if (checkBox != null)
                {
                    checkBox.Checked = checkBox.AttributeDataTag == selectedDataTag;
                }
            }
        }

        #endregion

    }
}
