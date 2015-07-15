function regis_mail() {
    var email = $('#Txtemail').val();
    var email_regex = /^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$/;
    if (!email_regex.test(email)) {
        alert("Email chưa đúng định dạng");
        return false;
    }
    else {
        $('#loadingemail').html("<img src='../vi-vn/Images/loading.gif' alt=''/>").fadeIn('fast');
        $.ajax({
            type: "POST",
            url: "../vi-vn/Ajax-customer.aspx/regis_mail",
            data: "{email:'" + email + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (e) {
                $('#loadingemail').fadeOut('fast');
                if (e.d == "success") {
                    //$('#get_email').addClass("hidden");
                    $('#regis_mail_success').html("Bạn đã đăng ký thành công.");
                }
                else {
                    alert("Email đã tồn tại")
                }
            }
        });
    }
}