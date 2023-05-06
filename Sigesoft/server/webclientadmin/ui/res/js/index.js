
var _menuStyle = F.cookie('MenuStyle') || 'tree';

var _mainTabs = F.cookie('MainTabs') || 'multi';

var SIDEBAR_WIDTH_CONSTANT = 260;

var _sidebarWidth = SIDEBAR_WIDTH_CONSTANT;



function isSmallWindowWidth() {
    var windowWidth = $(window).width();
    return windowWidth < 992;
}

function checkMobileView() {
    var bodyEl = $('body');
    var isMobileView = bodyEl.hasClass('mobileview');
    var needLayout = false;


    if (isSmallWindowWidth()) {
        if (!isMobileView) {
            bodyEl.addClass('mobileview');
            F.viewPortExtraWidth = SIDEBAR_WIDTH_CONSTANT;
            needLayout = true;
        }
    } else {
        if (isMobileView) {
            bodyEl.removeClass('mobileview');
            F.viewPortExtraWidth = 0;
            needLayout = true;
        }
    }

    if (needLayout) {
        if (!isMobileView) {
            _sidebarWidth = SIDEBAR_WIDTH_CONSTANT;
            if (getFoldButtonStatus()) {
                toggleSidebar(false);
            } else {

                F(PARAMS.sidebarRegion).setWidth(_sidebarWidth);
            }
            setFoldButtonStatus(true);
        } else {
            hideSidebarAndMask();
            setFoldButtonStatus(false);
        }

        F(PARAMS.mainPanel).doLayout();
    }

    // Do not enable animation for the first check
    if (!bodyEl.hasClass('mobileview-transition')) {
        bodyEl.addClass('mobileview-transition');
    }

}

function hideSidebarAndMask() {
    $('.mainpanel').removeClass('showsidebar');
    $('.bodyregion .showsidebar-mask').hide();
    setFoldButtonStatus(true);
}


function showSidebarAndMask() {
    $('.mainpanel').addClass('showsidebar');
    $('.bodyregion .showsidebar-mask').show();
    setFoldButtonStatus(false);
}


function onFoldClick(event) {
    if (isSmallWindowWidth()) {
        var sidebarregionEl = $('.sidebarregion');
        var bodyregionEl = $('.bodyregion');


        var maskEl = bodyregionEl.find('.showsidebar-mask');
        if (!maskEl.length) {
            maskEl = $('<div class="showsidebar-mask"></div>').appendTo(bodyregionEl.find('>.f-panel-bodyct'));
            maskEl.on('click', function () {
                if (isSmallWindowWidth()) {
                    hideSidebarAndMask();
                }
            });

            sidebarregionEl.on('click', '.leftregion .f-tree-node-leaf', function () {
                if (isSmallWindowWidth()) {
                    hideSidebarAndMask();
                }
            });
        }

        if (getFoldButtonStatus()) {
            showSidebarAndMask();
        } else {
            hideSidebarAndMask();
        }

    } else {
        toggleSidebar();
    }
}


function setFoldButtonStatus(collapsed) {
    var foldButton = F(PARAMS.btnCollapseSidebar);
    if (collapsed) {
        foldButton.setIconFont('f-iconfont-unfold');
    } else {
        foldButton.setIconFont('f-iconfont-fold');
    }
}


function getFoldButtonStatus() {
    var foldButton = F(PARAMS.btnCollapseSidebar);
    return foldButton.iconFont === 'f-iconfont-unfold';
}
function onThemeSelectClick(event) {
    var windowThemeRoller = F(PARAMS.windowThemeRoller);
    windowThemeRoller.show();
}


function onLoadingSelectClick(event) {
    var windowLoadingSelector = F(PARAMS.windowLoadingSelector);
    windowLoadingSelector.show();
}


function setCookie(name, value) {
    F.cookie(name, value, {
        expires: 100  // Unit: Day
    });
}



function expandSidebar() {
    toggleSidebar(false);
}


function collapseSidebar() {
    toggleSidebar(true);
}


function toggleSidebar(collapsed) {
    var sidebarRegion = F(PARAMS.sidebarRegion);
    var treeMenu = F(PARAMS.treeMenu);
    var logoEl = sidebarRegion.el.find('.logo');

    var currentCollapsed = getFoldButtonStatus();
    if (F.isUND(collapsed)) {
        collapsed = !currentCollapsed;
    } else {
        if (currentCollapsed === collapsed) {
            return;
        }
    }

    F.noAnimation(function () {

        setFoldButtonStatus(collapsed);

        if (!collapsed) {
            if (_menuStyle === 'tree') {
                logoEl.removeClass('short').text(logoEl.attr('title'));
                sidebarRegion.setWidth(_sidebarWidth);
                sidebarRegion.setSplitDraggable(true);

                treeMenu.miniMode = false;

                treeMenu.loadData();
            } else {
                sidebarRegion.expand();
            }
        } else {
            if (_menuStyle === 'tree') {
                logoEl.addClass('short').text('F');
                sidebarRegion.setWidth(60);
                sidebarRegion.setSplitDraggable(false);

                treeMenu.miniMode = true;

                treeMenu.loadData();
            } else {
                sidebarRegion.collapse();
            }
        }
    });
}

function onSidebarSplitDrag(event) {
    _sidebarWidth = this.width;
}


function onSearchTrigger1Click(event) {
    F.removeCookie('SearchText');
    top.window.location.reload();
}

function onSearchTrigger2Click(event) {
    var ttbxSearch = this;
    if (ttbxSearch.el.hasClass('collapsed')) {
        ttbxSearch.el.removeClass('collapsed').addClass('expanded').outerWidth(200);
    } else {
        var ttbxSearchValue = ttbxSearch.getValue();
        if (ttbxSearchValue) {
            setCookie('SearchText', this.getValue());
            top.window.location.reload();
        }
    }
}

function onSearchBlur(event) {
    var ttbxSearch = this;
    if (!ttbxSearch.getValue()) {
        ttbxSearch.el.removeClass('expanded').addClass('collapsed').outerWidth(24);
    }
}

// Click the title bar tool icon - View source code
function onToolSourceCodeClick(event) {
    var mainTabStrip = F(PARAMS.mainTabStrip);
    var windowSourceCode = F(PARAMS.windowSourceCode);


    var activeTab = mainTabStrip.getActiveTab();
    var iframeWnd, iframeUrl;
    if (activeTab.iframe) {
        iframeWnd = activeTab.getIFrameWindow();
        iframeUrl = activeTab.getIFrameUrl();
    }

    var files = [iframeUrl];
    var sourcefilesNode = $(iframeWnd.document).find('head meta[name=sourcefiles]');
    if (sourcefilesNode.length) {
        $.merge(files, sourcefilesNode.attr('content').split(';'));
    }
    windowSourceCode.show(PARAMS.sourceUrl + '?files=' + encodeURIComponent(files.join(';')));

}

// Click the title bar tool icon - Refresh
function onToolRefreshClick(event) {
    var mainTabStrip = F(PARAMS.mainTabStrip);

    var activeTab = mainTabStrip.getActiveTab();
    if (activeTab.iframe) {
        var iframeWnd = activeTab.getIFrameWindow();
        iframeWnd.location.reload();
    }
}

// Click the title bar tool icon - Open in new tab
function onToolNewWindowClick(event) {
    var mainTabStrip = F(PARAMS.mainTabStrip);

    var activeTab = mainTabStrip.getActiveTab();
    if (activeTab.iframe) {
        var iframeUrl = activeTab.getIFrameUrl();
        iframeUrl = PARAMS.processNewWindowUrl(iframeUrl);
        window.open(iframeUrl, '_blank');
    }
}


// Add sample tab (Find in tree by href)
function addExampleTabByHref(href, actived) {
    var mainTabStrip = F(PARAMS.mainTabStrip);
    var treeMenu = F(PARAMS.treeMenu);

    F.addMainTabByHref(mainTabStrip, treeMenu, href, actived);
}


// Add sample tab
// id:  Tab ID
// iframeUrl: Tab IFrame address 
// title:  Tab title
// icon:  Tab icon
// createToolbar:  callback function before creating tab (accept tabOptions parameter)
// refreshWhenExist:  When you add tabs, if the tabs already exist, refresh the internal IFrame
// iconFont:  Tab icon font
function addExampleTab(tabOptions, actived) {

    if (typeof (tabOptions) === 'string') {
        tabOptions = {
            id: arguments[0],
            iframeUrl: arguments[1],
            title: arguments[2],
            icon: arguments[3],
            createToolbar: arguments[4],
            refreshWhenExist: arguments[5],
            iconFont: arguments[6]
        };
    }

    F.addMainTab(F(PARAMS.mainTabStrip), tabOptions, actived);
}


// Remove the selected tab
function removeActiveTab() {
    var mainTabStrip = F(PARAMS.mainTabStrip);

    var activeTab = mainTabStrip.getActiveTab();
    activeTab.hide();
}

// Get the ID of the current active tab
function getActiveTabId() {
    var mainTabStrip = F(PARAMS.mainTabStrip);

    var activeTab = mainTabStrip.getActiveTab();
    if (activeTab) {
        return activeTab.id;
    }
    return '';
}

// Active tab, and refresh the content, Examples: Grid Control-> Miscellaneous-> Open in a New Tab (refresh the parent tab after closing)
function activeTabAndRefresh(tabId) {
    var mainTabStrip = F(PARAMS.mainTabStrip);
    var targetTab = mainTabStrip.getTab(tabId);
    var oldActiveTabId = getActiveTabId();

    if (targetTab) {
        mainTabStrip.activeTab(targetTab);
        targetTab.refreshIFrame();

        // Delete previous active tab
        mainTabStrip.removeTab(oldActiveTabId);
    }
}

// Active tab, and refresh the content, Examples: Grid Control-> Miscellaneous-> Open in a New Tab (update the grid in the Parent tab after closing)
function activeTabAndUpdate(tabId, param1) {
    var mainTabStrip = F(PARAMS.mainTabStrip);
    var targetTab = mainTabStrip.getTab(tabId);
    var oldActiveTabId = getActiveTabId();

    if (targetTab) {
        mainTabStrip.activeTab(targetTab);
        targetTab.getIFrameWindow().updatePage(param1);

        // Delete previous active tab
        mainTabStrip.removeTab(oldActiveTabId);
    }
}

// Notify dialog box
function notify(msg) {
    F.notify({
        message: msg,
        messageIcon: 'information',
        target: '_top',
        header: false,
        displayMilliseconds: 3 * 1000,
        positionX: 'center',
        positionY: 'center'
    });
}

// Click - Menu Style
function onMenuStyleCheckChange(event, item, checked) {
    var menuStyle = item.getAttr('data-tag');

    setCookie('MenuStyle', menuStyle);
    top.window.location.reload();
}

// Click - Display Mode
function onMenuDisplayModeCheckChange(event, item, checked) {
    var displayMode = item.getAttr('data-tag');

    setCookie('DisplayMode', displayMode);
    top.window.location.reload();
}

// Click - Language
function onMenuLangCheckChange(event, item, checked) {
    var lang = item.getAttr('data-tag');

    setCookie('Language', lang);
    top.window.location.reload();
}


function onMenuMainTabsCheckChange(event, item, checked) {
    var mainTabs = item.getAttr('data-tag');

    setCookie('MainTabs', mainTabs);
    top.window.location.reload();
}
function getExamplesCount() {
    return F(PARAMS.hfExamplesCount).getValue();
}


function generateBreadcrumbHtml(treeMenu, nodeId) {
    var result = [];
    var nodePaths = treeMenu.getNodePath(nodeId).split('/');
    if (nodePaths && nodePaths.length) {
        var nodePathLength = nodePaths.length;
        $.each(nodePaths, function (index, item) {
            if (item === 'root') {
                //result.push('<span class="breadcrumb-root">首页</span>');
            } else {
                var cls = 'breadcrumb-text';
                if (index === nodePathLength - 1) {
                    cls = 'breadcrumb-last';
                }
                var itemData = treeMenu.getNodeData(item);
                result.push('<span class="' + cls + '">' + itemData.text + '</span>');
            }
        });
    }
    return result.join('<span class="breadcrumb-separator">/</span>');
}

F.ready(function () {
    var mainTabStrip = F(PARAMS.mainTabStrip);
    var treeMenu = F(PARAMS.treeMenu);
    if (!treeMenu) return;



    // Initialize the interaction between tree and tabstrip, and update the browser address bar
    // treeMenu:  Tree control instance in the page
    // mainTabStrip:  TabStrip instance
    // updateHash: If you switch tab, update the browser address bar hash value (default:true)
    // refreshWhenExist:  When you add tabs, if the tabs already exist, refresh the internal IFrame (default: false)
    // refreshWhenTabChange: Whether to refresh the internal iframe when switching tabs (default:false)
    // maxTabCount: Maximum allowed number of tabs open
    // maxTabMessage: Tips for exceeding the maximum allowed number of tabs open
    // beforeNodeClick: Execute before the node click event (returns false without performing a node click event)
    var initOptions = {
        maxTabCount: 10,
        maxTabMessage: 'Please close some tabs (up to 10) first!',
        beforeNodeClick: function (event, treeNodeId) {
            var nodeEl = treeMenu.getNodeEl(treeNodeId);
            var nodeTag = nodeEl.attr('data-tag');
            var nodeData = treeMenu.getNodeData(treeNodeId);
            if (nodeTag === 'pop-window1') {
                F(PARAMS.windowThemeRoller).show();
                return false;
            } else if (nodeTag === 'newtab') {
                window.open(nodeData.href, '_blank');
                return false;
            }
        },
        beforeTabAdd: function (event, tabOptions, treeNodeId) {
            // Manually call F.addMainTab will also run here, because it is not triggered by the click tree node, so the treeNodeId is empty at this time
            if (!treeNodeId) {
                return;
            }
            var nodeEl = treeMenu.getNodeEl(treeNodeId);
            var nodeTag = nodeEl.attr('data-tag');
            if (nodeTag === 'custom-title') {
                var parentNode = treeMenu.getParentData(treeNodeId);
                tabOptions.title = parentNode.text + ' - ' + nodeEl.text();
            }

            // Single tab - displays the path where the current page is located
            if (_mainTabs === 'single') {
                $('#breadcrumb .breadcrumb-inner').html(generateBreadcrumbHtml(treeMenu, treeNodeId));
            }
        }
    };


    if (_mainTabs === 'single') {
        $('body').addClass('maintabs-single');


        $.extend(initOptions, {
            singleTabId: PARAMS.tabHomepage,
            refreshWhenExist: true
        });
    }

    F.initTreeTabStrip(treeMenu, mainTabStrip, initOptions);


    checkMobileView();

    F.windowResize(function () {
        checkMobileView();
    });


    if (isSmallWindowWidth()) {
        setFoldButtonStatus(true);
    }

    var hashFragment = window.location.hash.substr(1);
    if (!hashFragment || hashFragment.indexOf(PARAMS.mainUrl) >= 0) {
        addExampleTabByHref(PARAMS.dashboardUrl);
    }


});