function deletePurchaseOrder(event) {
    //event.preventDefault();
    console.log($(event).data('item'));
    var currenturl = $(event).data('item');

    /*$.ajax({
        type: "POST",
        url: currenturl,
        success: function (data) {
            if (data.success) {
                *//*$("#messageDiv").text(data.message).css("color", "green");
                setTimeout(function () {
                    location.reload(); // Reload the page after a short delay
                }, 1000); // Adjust the delay time if needed*//*
                alert("Hello");
            } else {
                *//*$("#messageDiv").text(data.message).css("color", "red");*//*
                alert("No Success");
            }
        },
        error: function () {
            *//*$("#messageDiv").text("An error occurred while processing the request.").css("color", "red");*//*
            alert("ERROR");
        }
    });*/
}

$(".deletePurchaseOrder").click(function (event) {
    
    

    
});
