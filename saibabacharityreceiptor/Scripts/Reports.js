$(function () {
    edit_delete();
    function edit_delete() {
        $('.edit_button').fancybox({
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'type': 'iframe',
            'autoScale': 'true',
            'height': 320,
            'width': 490,
            'autoDimensions': 'false',
            'onClosed': function () {
                var self = $(this)[0].orig;
                var tr = self.parents('tr').eq(0);
                var img = self.find('img');
                img.css({ 'width': 'auto', 'height': 'auto' });
                $.ajax({
                    type: 'POST',
                    url: '/Home/EditReceipt/' + tr.attr('id'),
                    data: "{'userid':'" + tr.attr('id') + "'}",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                        img.attr('src', '/images/ajax-loader.gif');
                    },
                    success: function (data) {
                        if (data != "") {
                            var index = tr.find('td').eq(0).html();
                            tr.html(data);
                            tr.find('td').eq(0).html(index);
                            edit_delete();
                        }
                        img.attr('src', '/Images/edit.gif');
                    },
                    error: function (e) {
                        img.attr('src', '/Images/edit.gif');
                    }
                });
            }
        }
        );
        $('.delete_button').css({ 'cursor': 'pointer' }).click(function () {
            if (confirm("Are you sure to remove this receipt entry?")) {
                var This = $(this);
                $.ajax({
                    type: 'POST',
                    url: '/Home/Deletereport?recpId=' + This.parents('tr').eq(0).attr('id'),
                    data: "{'recpId':'" + This.parents('tr').eq(0).attr('id') + "'}",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                        This.find('img').css({ 'width': 'auto', 'height': 'auto' });
                        This.find('img').attr('src', '/images/ajax-loader.gif');
                    },
                    success: function (data) {
                        if (data == "removed") {
                            This.parents('tr').eq(0).fadeIn(500).delay(500).remove();
                        }
                        else if (data == "loggedout") {
                            window.location = "/Controlpanel/LogOn";
                        }
                        else {
                            alert("An unknown error has been raised, please refresh the page and try again");
                            This.attr('src', '/images/product-images/icon/ico-delete.gif');
                        }
                    },
                    error: function (e) {
                        This.attr('src', '/images/product-images/icon/ico-delete.gif');
                    }
                });
            }
        });
    }
});