$(document).ready(function () {

})
//Cookie
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

//Phân trang, baseElement: tag sẽ phân trang, totalPages: tổng số record của entitied, url: url get data
function phantrang(baseElement, totalPages, url) {
    $(baseElement).pagination({
        items: totalPages,
        itemsOnPage: 5,
        currentPage: parseInt(new URL(document.URL).searchParams.get("page")),
        cssStyle: 'light-theme',
        hrefTextPrefix: url,
        hrefTextSuffix: '',
        onPageClick(pageNumber, event) {
            let urls = document.URL;
            let pageCurrent = new URL(urls).searchParams.get("page");
            urls = Boolean(pageCurrent) ? urls.replace("?page=" + pageCurrent, "?page=" + pageNumber) : urls + '?page=' + pageNumber;
            window.location.assign(urls);
        },
    });
}

//Alert success, sau khi thông báo thì reload();
function alertSuccess() {
    Swal.fire({
        title: 'Successfully.',
        html: '',
        showConfirmButton: false,
        icon: 'success'
    }).then(
        function (isConfirm) {
            if (isConfirm) {
                location.reload();
            }
        },
    );
}

function alertSuccess(text) {
    Swal.fire({
        title: 'Successfully.',
        html: text,
        showConfirmButton: false,
        icon: 'success'
    }).then(
        function (isConfirm) {
            if (isConfirm) {
                location.reload();
            }
        },
    );
}

function alertSuccessRedirect(text, location) {
    Swal.fire({
        title: 'Successfully.',
        html: text,
        showConfirmButton: false,
        icon: 'success'
    }).then(
        function (isConfirm) {
            if (isConfirm) {
                window.location = location;
            }
        },
    );
}

function alertSuccessNonReload() {
    Swal.fire({
        title: 'Successfully.',
        html: '',
        showConfirmButton: false,
        icon: 'success'
    })
}

function alertError() {
    Swal.fire({
        icon: 'error',
        title: 'Failed...',
        html: 'Something went wrong!',
    });
}

function alertError(texterror) {
    Swal.fire({
        icon: 'error',
        title: 'Failed...',
        html: texterror,
    });
}

function alertQuestion(titleStr, textStr) {
    Swal.fire({
        title: titleStr,
        html: textStr,
        icon: 'question'
    });
}

// alert warning, cảnh báo trả về kết quả lực chọn
function alertWarning() {
    return Swal.fire({
        title: 'Are you sure?',
        html: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    });
}
