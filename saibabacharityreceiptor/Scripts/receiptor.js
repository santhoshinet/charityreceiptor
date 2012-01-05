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
    $('.txtdonationinwords,.date,.txtdonationamount,.txtcontact,.txtdate').bind('keypress', function (e) {
        if (e.which > 44 && e.which < 58)
            return true;
        return false;
    });
});