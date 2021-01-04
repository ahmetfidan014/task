var product = function () {
    var deletingItem = -1;
    var handle = function () {
        if ($('input[name=Barcode]').val() === '') {
            $('input[name=Barcode]').val(random(8680000000000, 8689999999999));
        }
        $("#Product_form").validate({
            rules: {
                email: { email: true }
            },
            submitHandler: function (form) {
                product_send($(form));
            }
        });

        var imagesPreview = function (input, placeToInsertImagePreview) {
            $(placeToInsertImagePreview).html("");
            if (input.files) {
                var filesAmount = input.files.length;

                for (i = 0; i < filesAmount; i++) {
                    var reader = new FileReader();

                    reader.onload = function (event) {
                        $($.parseHTML('<img>')).attr('src', event.target.result).css({ 'width': '150px', 'margin': '15px', 'position': 'relative' }).after('<i class="fas fa-folder-minus"></i>').appendTo(placeToInsertImagePreview);
                    }
                    reader.readAsDataURL(input.files[i]);
                }
            }

        };
        $('#product-photo-add').on('change', function () {
            imagesPreview(this, '.photos_div');
        });
        var product_send = function (form) {
            $(".loading-popup").addClass("active");
            setTimeout(function () {
                var formData = new FormData(form[0]);
                $.ajax({
                    url: '/Admin/Product/ProductAdd',
                    type: 'POST',
                    data: formData,
                    async: false,
                    success: function (data) {
                        if (data.result === true) {
                            $(".loading-popup").removeClass("active");
                            window.location.href = "/Admin/Product/Products";
                        }
                        else {
                            $(".loading-popup").removeClass("active");
                            $(".popup.hata-popup .overlay .hata-icerik p").text(data.message);
                            $(".popup.hata-popup").addClass("active");
                            $("#Product_form")[0].reset();
                        }

                    },
                    cache: false,
                    contentType: false,
                    processData: false
                });
            }, 500);
        };      
    };

   
    function DeletePhoto(Id) {
        $(".loading-popup").addClass("active");
        setTimeout(function () {
            $.ajax({
                url: "/Admin/Product/PhotoDelete",
                data: { id: Id },
                dataType: "json",
                type: "POST",
                success: function (data) {
                    if (data.result === true) {
                        location.reload();
                    }
                    else {
                        $(".loading-popup").removeClass("active");
                        $(".popup.hata-popup .overlay .hata-icerik p").text(data.message);
                        $(".popup.hata-popup").addClass("active");
                    }
                }
            });
        }, 500);
    }
    $(document).on("click", ".fas.fa-folder-minus", function (e) {
        deletingItem = $(this).data("id");
        DeletePhoto(deletingItem);
    });
    var random = function (min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    };

    return {
        init: function () { handle(); }
    };

}(jQuery);