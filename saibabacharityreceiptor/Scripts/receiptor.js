$(function () {
    $('.txtreceiptno').focus();
    $('input[type="submit"]').click(function () {
        $('.text,.txtdate').removeClass('errorinfo');
        if ($('.txtreceiptno').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtdate').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtname').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtmi').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtlastname').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtaddress').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtcity').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtstate').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtzipcode').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtemail').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtemail').validateEmail({ cssclass: "errorinfo", alert: true })
            && $('.txtcontact').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtmerchandiseItem').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtquantity').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtservicetype').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txthoursserved').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtfmvvalue').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtdonationamount').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtdonationinwords').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtmerchandiseItem').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtvalue').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txthoursserved').validateText({ cssclass: "errorinfo", alert: true })) {
            if ($('.txthoursserved').size() > 0 && $('.txthoursserved').val() != "" && parseInt($('.txthoursserved').val()) < 1) {
                alert("Please Input no of hours");
                $('.txthoursserved').addClass('errorinfo');
                $('.txthoursserved').focus();
                return false;
            }
            if ($('.cmbModeOfPayment').size() > 0 && $('.cmbModeOfPayment')[0].selectedIndex == 0) {
                alert("Please select mode of payment");
                $('.cmbModeOfPayment').addClass('errorinfo');
                $('.cmbModeOfPayment').focus();
                return false;
            }
            if ($('.CmbDonationReceivedBy')[0].selectedIndex == 0) {
                alert("Please select donation received by member");
                $('.CmbDonationReceivedBy').addClass('errorinfo');
                $('.CmbDonationReceivedBy').focus();
                return false;
            }
        }
        else
            return false;
        return true;
    });
    $('.txtdate,.txtdonationamount,.txtcontact,.txtdate,.txtzipcode').bind('keypress', function (e) {
        if ((e.which > 44 && e.which < 58) || e.which == 8 || e.which == 0)
            return true;
        return false;
    });
    $('.txtdate').datepicker({
        onSelect: function (input, inst) {
            $('.txtdate').datepicker("option", "dateFormat", "mm/dd/yy");
        }
    });
    $('.txtrecurrencedates').datepicker({
        onSelect: function (input, inst) {
            $('.txtrecurrencedates').datepicker("option", "dateFormat", "mm/dd/yy");
        }
    });
    $('.txtdate,.txtrecurrencedates').val(getdate());
    function getdate() {
        var d = new Date();
        var curr_date = d.getDate();
        var curr_month = d.getMonth();
        curr_month++;
        var curr_year = d.getFullYear();
        var retVal = "";
        if (curr_month < 10)
            retVal += "0" + curr_month;
        else
            retVal += curr_month;
        retVal += "/";
        if (curr_date < 10)
            retVal += "0" + curr_date;
        else
            retVal += curr_date;
        retVal += "/";
        if (curr_year < 10)
            retVal += "0" + curr_year;
        else
            retVal += curr_year;
        return retVal;
    }
    var Index = 1;
    $('.btnaction').live('click', function () {
        var reccurence = $(this).parents('li').eq(0);
        var clone = reccurence.clone();
        reccurence.after(clone);
        clone.find('.btnactionremove').remove();
        clone.append("<span class='btnactionremove'>-</span>");
        Index++;
        var recclass = "reccurenceDates" + Index.toString();
        clone.find('input[type="text"]').attr('class', recclass + " " + "text" + " " + "smallbox").attr('id', '');
        $('.' + recclass).datepicker({
            onSelect: function (input, inst) {
                $('.' + recclass).datepicker("option", "dateFormat", "mm/dd/yy");
            }
        });
    });
    $('.btnactionremove').live('click', function () {
        $(this).parents('li').eq(0).remove();
    });
});