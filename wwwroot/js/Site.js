function laDienThoai() {
    return window.matchMedia("(max-width: 600px)").matches;
}
function toggleMobileMenu() {
    var menu = document.getElementById("mainNav");
    var nutMenu = document.querySelector(".menu-toggle");
    var khungTimKiem = document.getElementById("headerSearch");

    if (!menu || !nutMenu) return;

    // Mở menu thì đóng ô tìm kiếm
    if (khungTimKiem) khungTimKiem.classList.remove("is-open");

    var dangMo = menu.classList.toggle("is-open");

    nutMenu.setAttribute("aria-expanded", dangMo ? "true" : "false");
    nutMenu.setAttribute("aria-label", dangMo ? "Đóng menu" : "Mở menu");

    var icon = nutMenu.querySelector("i");
    if (icon) {
        icon.className = dangMo
            ? "fa-solid fa-xmark"
            : "fa-solid fa-bars";
    }
}

// XỬ LÝ KHI BẤM ICON TÌM KIẾM
function xuLyTimKiem() {
    var khungTimKiem = document.getElementById("headerSearch");
    var oTimKiem = document.getElementById("txtTimKiem");

    if (!khungTimKiem || !oTimKiem) return;

    // Điện thoại: lần bấm đầu mở ô nhập để gõ chữ
    if (laDienThoai() && !khungTimKiem.classList.contains("is-open")) {
        // đóng menu nếu đang mở
        var menu = document.getElementById("mainNav");
        if (menu && menu.classList.contains("is-open")) toggleMobileMenu();

        khungTimKiem.classList.add("is-open");
        oTimKiem.focus();
        return;
    }

    // Ô đang mở nhưng chưa gõ gì: điện thoại thì đóng lại
    if (laDienThoai() && !oTimKiem.value.trim()) {
        khungTimKiem.classList.remove("is-open");
        return;
    }

    // Đã có chữ thì thực hiện tìm
    timKiemBanh();
}

// Địa chỉ trang Sản phẩm: lấy từ link "Sản phẩm" trong menu (đã được server tính sẵn đường dẫn đúng)
function layDiaChiSanPham() {
    var khungTimKiem = document.getElementById("headerSearch");
    var diaChi = khungTimKiem ? khungTimKiem.getAttribute("data-url") : "";

    if (diaChi) return diaChi;

    var lienKet = document.querySelector('#mainNav a[href*="SanPham"]');

    if (lienKet) {
        return lienKet.href.split("?")[0].split("#")[0];
    }

    return "SanPham.aspx";
}

// TÌM KIẾM BÁNH: luôn chuyển sang trang Sản phẩm bằng GET:  SanPham.aspx?q=từ khóa
// Trang Sản phẩm (SanPham.aspx.cs) sẽ lọc theo từ khóa, không phân biệt dấu.
function timKiemBanh() {
    var oTimKiem = document.getElementById("txtTimKiem");

    if (!oTimKiem) return;

    var tuKhoa = oTimKiem.value.trim();

    if (!tuKhoa) {
        alert("Cậu hãy nhập tên bánh cần tìm nhé!");
        oTimKiem.focus();
        return;
    }

    // #khuDanhSach: tải xong tự cuộn tới danh sách bánh
    window.location.href =
        layDiaChiSanPham() + "?q=" + encodeURIComponent(tuKhoa) + "#khuDanhSach";
}

document.addEventListener("DOMContentLoaded", function () {

    // ĐÁNH DẤU MENU THEO TRANG ĐANG MỞ
    // Chuẩn hóa đường dẫn: bỏ .aspx, bỏ "/default", bỏ dấu "/" cuối
    // để /Default, /Default.aspx và / đều là trang chủ,
    // /SanPham và /SanPham.aspx đều là trang sản phẩm.
    function chuanHoaDuongDan(p) {
        p = p.toLowerCase().replace(/\.aspx$/, "");
        if (p.endsWith("/default")) p = p.slice(0, -8);
        p = p.replace(/\/+$/, "");
        return p || "/";
    }

    var duongDanHienTai = chuanHoaDuongDan(window.location.pathname);

    document.querySelectorAll("#mainNav a").forEach(function (muc) {
        var duongDanMenu = chuanHoaDuongDan(new URL(muc.href).pathname);

        if (duongDanMenu === duongDanHienTai) {
            muc.classList.add("active");
            muc.setAttribute("aria-current", "page");
        } else {
            muc.classList.remove("active");
            muc.removeAttribute("aria-current");
        }
    });

    var oTimKiem = document.getElementById("txtTimKiem");

    if (oTimKiem) {
        oTimKiem.addEventListener("keydown", function (event) {
            if (event.key === "Enter") {
                event.preventDefault();
                timKiemBanh();
            }
        });
    }

    // Bấm ra ngoài thì đóng ô tìm kiếm (điện thoại)
    document.addEventListener("click", function (event) {
        var khungTimKiem = document.getElementById("headerSearch");

        if (khungTimKiem && laDienThoai() &&
            !khungTimKiem.contains(event.target)) {
            khungTimKiem.classList.remove("is-open");
        }
    });

    // Có từ khóa trên URL (?q=...) thì điền lại vào ô tìm kiếm,
    // để biết trang đang lọc theo từ khóa nào (việc lọc do server làm).
    var tuKhoaURL = new URLSearchParams(window.location.search).get("q");

    if (tuKhoaURL && oTimKiem) {
        oTimKiem.value = tuKhoaURL;
    }
});

// ĐÓNG MENU KHI CHUYỂN VỀ GIAO DIỆN MÁY TÍNH
window.addEventListener("resize", function () {
    if (window.innerWidth > 600) {
        var menu = document.getElementById("mainNav");
        var nutMenu = document.querySelector(".menu-toggle");
        var khungTimKiem = document.getElementById("headerSearch");

        if (menu) menu.classList.remove("is-open");
        if (khungTimKiem) khungTimKiem.classList.remove("is-open");

        if (nutMenu) {
            nutMenu.setAttribute("aria-expanded", "false");

            var icon = nutMenu.querySelector("i");

            if (icon) {
                icon.className = "fa-solid fa-bars";
            }
        }
    }
});