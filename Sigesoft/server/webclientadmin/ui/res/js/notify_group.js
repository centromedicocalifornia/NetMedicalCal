

(function () {


    var _orderNumber = 1, _notifySpace = 5, _notifies = [];


    function onNotifyHide() {

        clearNotifiesAnimation();

        var notify = this;
        var notifyHeight = notify.el.outerHeight(true) + _notifySpace;
        var notifyIndex = $.inArray(notify, _notifies);
        _notifies.splice(notifyIndex, 1);

        var count = _notifies.length;
        if (count) {
            for (var i = notifyIndex; i < count; i++) {
                var item = _notifies[i];
                item.top -= notifyHeight;
                item.el.animate({
                    'top': item.top
                });
            }

            _notifies.sort(function (a, b) {
                return a.top - b.top;
            });
        }
    }


    function moveNotifiesDown(newNotify, fn) {
        
        clearNotifiesAnimation();

        var count = _notifies.length, finished = 0;
        if (!count) {
            fn.apply(window);
            return;
        }

        var notifyHeight = newNotify.el.outerHeight(true) + _notifySpace;
        for (var i = 0; i < count; i++) {
            var item = _notifies[i];
            item.top += notifyHeight;
            item.el.animate({
                'top': item.top
            }, function () {
                
                finished++;

                if (finished >= count) {
                    fn.apply(window);
                }
            });
        }
    }

    
    function clearNotifiesAnimation() {
        var count = _notifies.length;
        if (count) {
            for (var i = 0; i < count; i++) {
                var item = _notifies[i];
                var itemEl = item.el;
                if (itemEl.is(":animated")) {
                    itemEl.stop(false, true);
                }
            }
        }
    }
    
    function calcNotifyTop() {
        var top = _notifySpace;
        if (_notifies.length) {
            var lastNotify = _notifies[_notifies.length - 1];
            top += lastNotify.top + lastNotify.el.outerHeight(true);
        }
        return top;
    }


    window.showNotifyGroup = function (options, newestOnTop) {

        $.extend(options, {
            cls: 'notify-group-item',
            constrainInitialSize: false,
            positionX: 'right',
            listeners: {
                hide: onNotifyHide
            }
        });

        if (newestOnTop) {

            options.hidden = true;
            options.top = _notifySpace;
        } else {
            options.top = calcNotifyTop();
        }

        var notify = F.notify(options);

        if (newestOnTop) {
            moveNotifiesDown(notify, function () {
                notify.show();
            });
            _notifies.splice(0, 0, notify);
        } else {
            _notifies.push(notify);
        }
    }


})();