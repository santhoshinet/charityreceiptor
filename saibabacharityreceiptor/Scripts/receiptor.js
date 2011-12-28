$(function () {
    $('.txtreceiptno').focus();
    $('input[type="submit"]').click(function () {
        $('.text,.txtdate').removeClass('errorinfo');
        if ($('.txtreceiptno').validateText({ cssclass: "errorinfo", alert: true })
            && $('.date').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtname').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtaddress').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtemail').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtemail').validateEmail({ cssclass: "errorinfo", alert: true })
            && $('.txtcontact').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtdonationamount').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtdonationinwords').validateText({ cssclass: "errorinfo", alert: true })) {
            if ($('.cmbModeOfPayment')[0].selectedIndex == 0) {
                alert("Please select mode of payment");
                $('.cmbModeOfPayment').addClass('errorinfo');
                $('.cmbModeOfPayment').focus();
                return false;
            }
            if ($('.cmbdonationreceivedby')[0].selectedIndex == 0) {
                alert("Please select donation received by member");
                $('.cmbdonationreceivedby').addClass('errorinfo');
                $('.cmbdonationreceivedby').focus();
                return false;
            }
            var value = parseInt($('.txtdate').val());
            if (value > 31) {
                alert("Please input valid Date.");
                $('.txtdate').addClass('errorinfo');
                $('.txtdate').focus();
                return false;
            }
            value = parseInt($('.txtmonth').val());
            if (value > 12) {
                alert("Please input valid Date.");
                $('.txtmonth').addClass('errorinfo');
                $('.txtmonth').focus();
                return false;
            }
            value = parseInt($('.txtyear').val());
            if (value < 1960 || value > 2005) {
                alert("Please input valid year.");
                $('.txtyear').addClass('errorinfo');
                $('.txtyear').focus();
                return false;
            }
        }
        else
            return false;
        return true;
    });
    $('.txtmobile,.date').bind('keypress', function (e) {
        if (e.which > 47 && e.which < 58)
            return true;
        return false;
    });
});