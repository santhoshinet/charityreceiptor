$(function () {
    $('.txtreceiptno').focus();
    $('input[type="submit"]').click(function () {
        $('.text,.txtdate').removeClass('errorinfo');
        if ($('.txtreceiptno').validateText({ cssclass: "errorinfo", alert: true })
            && $('.txtdate').validateText({ cssclass: "errorinfo", alert: true })
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
    $('.txtdate,.txtdonationamount,.txtcontact,.txtdate').bind('keypress', function (e) {
        if ((e.which > 44 && e.which < 58) || e.which == 8 || e.which == 0)
            return true;
        return false;
    });
    $('.txtdate').datepicker({
        onSelect: function (input, inst) {
            $('.txtdate').datepicker("option", "dateFormat", "dd/mm/yy");
        }
    });
    $('.txtdate').val(getdate());
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
});