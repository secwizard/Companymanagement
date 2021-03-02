function ConfirmShow(yesFunction, noFunction, message, msgType) {
    try {

        //msgType = error, success, warning
        var msgHtml = '';
        if (msgType.toLowerCase() == 'error') {
            msgHtml = msgHtml + '<div class="padding-control-withmsgdivstyle error-msg mb-10"><label class="alert-icons-label error-sm-bg"><i class="fa fa-times-circle error-fa-color"></i></label>' + message + '</div>';
        }
        else if (msgType.toLowerCase() == 'success') {
            msgHtml = msgHtml + '<div class="padding-control-withmsgdivstyle sucess-msg mb-10"><label class="alert-icons-label success-sm-bg"><i class="fa fa-check-circle success-fa-color"></i></label>' + message + '</div>';
        }
        else if (msgType.toLowerCase() == 'warning') {
            msgHtml = msgHtml + '<div class="padding-control-withmsgdivstyle warning-msg mb-10"><label class="alert-icons-label warning-sm-bg"><i class="fa fa-exclamation-triangle warning-fa-color"></i></label>' + message + '</div>';
        }
        else {
            msgHtml = msgHtml + '<div class="padding-control-withmsgdivstyle info-msg mb-10"><label class="alert-icons-label info-sm-bg"><i class="fa fa-info-circle info-fa-color"></i></label>' + message + '</div>';
        }

        var dvButtonHtml = '';
        dvButtonHtml = dvButtonHtml + '<button class="primary-type-button min-widthauto" onclick="' + yesFunction + '; CloseMessage();">Yes</button>';
        dvButtonHtml = dvButtonHtml + '<button class="third-type-button mr-10 min-widthauto close-msg-popup" data-dismiss="modal"  onclick="' + noFunction + '; CloseMessage();">No</button>';

        $('#pMessageShow').html('');
        $('#pMessageShow').html(msgHtml);

        $('#dvButtonShow').html('');
        $('#dvButtonShow').html(dvButtonHtml);

        $('#popUpAlerts').modal("show");
    }
    catch (e) {
        console.log(e);
    }
}

function MessageShow(clickfunction, message, msgType) {
    try {
        //msgType = error, success, warning
        var msgHtml = '';
        if (msgType.toLowerCase() == 'error') {
            msgHtml = msgHtml + '<div class="padding-control-withmsgdivstyle error-msg mb-10"><label class="alert-icons-label error-sm-bg"><i class="fa fa-times-circle error-fa-color"></i></label>' + message + '</div>';
        }
        else if (msgType.toLowerCase() == 'success') {
            msgHtml = msgHtml + '<div class="padding-control-withmsgdivstyle sucess-msg mb-10"><label class="alert-icons-label success-sm-bg"><i class="fa fa-check-circle success-fa-color"></i></label>' + message + '</div>';
        }
        else if (msgType.toLowerCase() == 'warning') {
            msgHtml = msgHtml + '<div class="padding-control-withmsgdivstyle warning-msg mb-10"><label class="alert-icons-label warning-sm-bg"><i class="fa fa-exclamation-triangle warning-fa-color"></i></label>' + message + '</div>';
        }
        else {
            msgHtml = msgHtml + '<div class="padding-control-withmsgdivstyle info-msg mb-10"><label class="alert-icons-label info-sm-bg"><i class="fa fa-info-circle info-fa-color"></i></label>' + message + '</div>';
        }

        var dvButtonHtml = '';
        dvButtonHtml = dvButtonHtml + '<button class="primary-type-button min-widthauto" onclick="' + clickfunction + '; CloseMessage();">Ok</button>';

        $('#pMessageShow').html('');
        $('#pMessageShow').html(msgHtml);

        $('#dvButtonShowok').html('');
        $('#dvButtonShowok').html(dvButtonHtml);

        $('#popUpAlerts').modal("show");
        HideLoader();
    }
    catch (e) {
        console.log(e);
    }
}

function CloseMessage() {
    try {
       // $(".fullpage-message-div").hide();

        $('#popUpAlerts').modal("hide");
    }
    catch (e) {
        console.log(e);
    }
}