document.addEventListener("DOMContentLoaded", function () {
    var nutGiam = document.getElementById("giamSoLuong");
    var nutTang = document.getElementById("tangSoLuong");
    var oSoLuong = document.getElementById("soLuongBanh");

    nutTang.onclick = function () {
        var soLuong = parseInt(oSoLuong.value, 10);

        if (isNaN(soLuong) || soLuong < 1) {
            soLuong = 1;
        }

        oSoLuong.value = soLuong + 1;
    };

    nutGiam.onclick = function () {
        var soLuong = parseInt(oSoLuong.value, 10);

        if (isNaN(soLuong) || soLuong <= 1) {
            oSoLuong.value = 1;
        } else {
            oSoLuong.value = soLuong - 1;
        }
    };
});