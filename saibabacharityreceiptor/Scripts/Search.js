$(function () {
    $('.txtdate,.txtdonationamount,.txtcontact,.txtzipcode').bind('keypress', function (e) {
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
    $('#TxtStartDate').val(getdate($('#TxtStartDate').val()));
    $('#TxtEndDate').val(getdate($('#TxtEndDate').val()));
    function getdate(val) {
        var d = new Date(val);
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
    $('.prevNavigation,.nextNavigation').click(function () {
        $('#hdnPageindex').val($(this).attr('pageindex'));
        $('form').submit();
    });
});