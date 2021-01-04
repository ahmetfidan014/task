var login = function () {
    var handle = function () {
        $('#login_form').validate({
            rules: {
            },
            submitHandler: function (form) {
                login_send($(form));
            }
        });

        $(document).on("click", ".popup.hata-popup .btn-orange", function (e) {
            e.preventDefault();
            $(".popup.hata-popup").removeClass("active");
        });
      
    };

    var login_send = function (form) {
        $.post('/Login/login', form.serialize(), function (data) {
            if (data.result === true) {
                window.location.href = "/Admin/Home/Welcome";
            }
            else {
                $(".popup.hata-popup").addClass("active");
                $(".popup.hata-popup .hata-icerik").html("<p>" + data.message + "</p>");
            }
            grecaptcha.reset();
        });
    };


    return {
        init: function () { handle(); },
        login_send: function (form) { return login_send(); }
    };

}(jQuery);