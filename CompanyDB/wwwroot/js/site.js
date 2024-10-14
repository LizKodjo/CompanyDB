// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


    $(".custom-file-input").on("change", function () {
            var fileName = $(this).val().split("\\").pop();
    $(this).siblings("custom-file-label").addClass("selected").html(fileName);
        });

    function DeleteItem(btn) {
            var table = document.getElementById('EmpTable');
    var rows = table.getElementsByTagName('tr');

    if (rows.length == 2)
    {
        alert("Please don't delete this row");
         return;
    }

        var btnIdx = btn.id.replaceAll('btnremove-', '');
        var idofIsDeleted = btnIdx + "_IsDeleted";

        var hidIsDelId = document.querySelector("[id$='" + idofIsDeleted + "']").id;
        document.getElementById(hidIsDelId).value = "true";

        //$(btn).closest('tr').remove();

        $(btn).closest('tr').hide();
        }

    function AddItem(btn) {
        var table = document.getElementById('EmpTable');
        var rows = table.getElementsByTagName('tr');

        var rowOuterHtml = rows[rows.length - 1].outerHTML;

        var lastrowIdx = rows.length - 2;
        var nextrowIdx = eval(lastrowIdx) + 1;

   

        rowOuterHtml = rowOuterHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
        rowOuterHtml = rowOuterHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
        rowOuterHtml = rowOuterHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);

        var newRow = table.insertRow();
        newRow.innerHTML = rowOuterHtml;

        var x = document.getElementsByTagName("input");

        for (var i = 0; i < x.length; i++) {
            if (x[i].type == "text" && x[i].id.indexOf('_' + nextrowIdx + '_') > 0)
                x[i].value = '';
        }     
        }

    function rebindvalidators() {
            var $form = $("#CompanyForm");
    $form.unbind();
    $form.data("validator", null);
    $.validator.unobtrusive.parse($form);
    $form.validate($form.data("unobtrusiveValidastion").options);
        }

