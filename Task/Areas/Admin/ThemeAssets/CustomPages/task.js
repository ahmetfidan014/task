var task = function () {
    $(document).on("click", ".popup.hata-popup .btn-orange", function (e) {
        e.preventDefault();
        $(".popup.hata-popup").removeClass("active");
    });
    $(document).on("click", ".popup.bilgi-popup .btn-orange", function (e) {
        e.preventDefault();
        $(".popup.bilgi-popup").removeClass("active");
    });
    $(document).on("click", ".delete-button", function () {
        document.getElementById("id-delete").setAttribute("data-id", $(this).data("id"));
        document.getElementById("id-delete").setAttribute("data-table", $(this).data("table"));
        $(".popup.delete-popup").addClass("active");
    });


    $(document).on("click", ".popup.delete-popup .btn-red", function () {
        var itemId = $(this).data("id");
        var itemTable = $(this).data("table");
        deleteItem(itemId, itemTable);
    });

    $(document).on("click", ".popup.delete-popup .btn-orange", function (e) {
        e.preventDefault();
        $(".popup.delete-popup").removeClass("active");
    });

    var deleteItem = function (Id, Table) {
        $(".loading-popup").addClass("active");
        setTimeout(function () {
            $.ajax({
                url: "/Admin/" + Table + "/" + Table + "Delete",
                data: { id: Id },
                dataType: "json",
                type: "POST",
                success: function (data) {
                    if (data.result === true) {
                        console.log("test1");
                        location.reload();
                    }
                    else {
                        console.log("test2");
                        $(".loading-popup").removeClass("active");
                        $(".popup.hata-popup .overlay .hata-icerik p").text(data.message);
                        $(".popup.hata-popup").addClass("active");
                    }
                }
            });
        }, 500);
    };
    return {
        deleteItem: function (Id, Table) { return deleteItem(); }
    };
}(jQuery);

