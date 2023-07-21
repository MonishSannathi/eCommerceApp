$('#OrderFile').on('change', function (e) {
    var file = e.target.files[0];
    $('#filePreview').prop('disabled', false);
    // Create a URL for the PDF file
    var fileURL = URL.createObjectURL(file);
    // Set the source of the iframe to the PDF file URL
    //$('#pdfContainer').html('<iframe src="' + fileURL + '" width="100%" height="100%" frameborder="0"></iframe>');
    $('#pdfFrame').attr('src', fileURL);
});

$("#filePreview").change(function () {
    if ($(this).is(":checked")) {
        // Checkbox is checked, display the iframe
        $('#pdfContainer').prop('hidden', false);
    } else {
        // Checkbox is unchecked, hide the iframe
        $('#pdfContainer').prop('hidden', true);
    }
});