$(function () {
    edit_delete();
    // for Search operations
    $("#txtsearchcriteria").quicksearch("#Cart_Table tbody tr", {
        noResults: '#noresultsrow',
        stripeRows: ['odd', 'even'],
        loader: 'span.loading',
        onBefore: function () {
            $('#Cart_Table tbody tr').removeHighlight();
        },
        onAfter: function () {
            if ($('#txtsearchcriteria').val() != null && $('#txtsearchcriteria').val() != "") {
                $('#Cart_Table tbody tr').highlight($('#txtsearchcriteria').val());
            }
        }
    });
    $('.LnkAddnewuser').fancybox({
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'type': 'iframe',
        'autoScale': 'true',
        'height': 435,
        'width': 470,
        'autoDimensions': 'false'
    });
    function edit_delete() {
        $('.edit_button').fancybox({
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'type': 'iframe',
            'autoScale': 'true',
            'height': 435,
            'width': 470,
            'autoDimensions': 'false',
            'onClosed': function () {
                var self = $(this)[0].orig;
                var tr = self.parents('tr').eq(0);
                var img = self.find('img');
                img.css({ 'width': 'auto', 'height': 'auto' });
                $.ajax({
                    type: 'POST',
                    url: '/Controlpanel/Viewuserinfo/' + tr.attr('id'),
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
            if (confirm("It remove the product permanently from this category. Are you sure?")) {
                var This = $(this);
                $.ajax({
                    type: 'POST',
                    url: '/Controlpanel/Deleteuser?userid=' + This.parents('tr').eq(0).attr('id'),
                    data: "{'productid':'" + This.parents('tr').eq(0).attr('id') + "'}",
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