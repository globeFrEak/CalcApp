

// submit form on hitting ENTER
$(document).keypress(function (e) {
    if (e.which == 13) {
        $("#calc_form").submit();
    }
});

$(document).ready(function () {
    // save cursor position on input
    var $calc_input = $("#calc_input");
    function reportCursor(options) {
        val_lenght = $calc_input.val().length;
        options = options || '0';
        var sel = $calc_input.getSelection();
        switch (options) {
            case "add":
                sel = $calc_input.setSelection(sel.start + 1);
                break;
            case "sub":
                sel = $calc_input.setSelection(sel.start - 1);
                break;
            case "end":
                sel = $calc_input.setSelection(val_lenght);
                break;
        }
        if (val_lenght == 0) {
            $(".btn.posadd").addClass("disabled");
            $(".btn.possub").addClass("disabled");
        } else if (val_lenght == sel.start) {
            $(".btn.posadd").addClass("disabled");
            $(".btn.possub").removeClass("disabled");
        } else if (val_lenght > sel.start && sel.start == 0) {
            $(".btn.possub").addClass("disabled");
            $(".btn.posadd").removeClass("disabled");
        } else {
            $(".btn.posadd").removeClass("disabled");
            $(".btn.possub").removeClass("disabled");
        }
        return sel;
    }
    reportCursor("end");
    $(document).on("selectionchange", reportCursor);
    $calc_input.on("keyup input mouseup textInput", reportCursor);
    $calc_input.focus();

    // button value in input field
    $(".btn.calc").click(function () {        
        button = $(this).val();        
        $calc_input.replaceSelectedText(button);
    });

    // copy result to input field
    $(".insert_result").click(function () {
        value = $(this).val()
        pos = reportCursor();        
        $calc_input.insertText(value, pos.end);
    });

    // delete input on reset button
    $(".btn.reset").click(function () {
        $('#calc_input').attr('value', '');
        // remove pulse when click reset
        $(".btn.calc.closereq").removeClass("required");
    });

    // cursor position + 1 
    $(".btn.posadd").click(function () {
        if (!$(this).hasClass("disabled")) {
            reportCursor("add");
            $calc_input.focus();
        };
    });

    // cursor position - 1 
    $(".btn.possub").click(function () {
        if (!$(this).hasClass("disabled")) {
            reportCursor("sub");
            $calc_input.focus();
        };
    });    

    // focus input field after pressing science buttons
    $(".btn.calc.science").click(function () {
        $("#calc_input").focus();
    });

    // pulse ) button
    $(".btn.calc.open").click(function () {
        $(".btn.calc.closereq").addClass("required");
    });
    $(".btn.calc.closereq").click(function () {
        $(this).removeClass("required");
    });

    // enable tooltips
    $(function () {
        $('[data-toggle="tooltip"]').tooltip({ container: "body" });
    });

    // set label class at radio button active    
    $("input[name='Degree']:checked").closest("label.btn").addClass("active");
    

});