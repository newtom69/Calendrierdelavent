$(document).ready(function () {
    $('.picture').hide();
    $('.boxReplacingPicture').hide();
    
    $('.picture').click(function () {
        ScheduleHideButtonsCursor();
    });

    $('#closeButton').click(function () {
        DefaultScreen();
        $('#popupPicture').hide();
    });

    $('#defaultScreenButton').click(function () {
        DefaultScreen();
    });

    $('#maxScreenButton').click(function () {
        MaxScreen();
    });

    $(document).keydown(function () {
        KeyDown();
    });

    $("#previousArea").hover(function () {
        $('#previousButton').addClass("hover");
    }, function () {
        $('#previousButton').removeClass("hover");
    });

    $("#nextArea").hover(function () {
        $('#nextButton').addClass("hover");
    }, function () {
        $('#nextButton').removeClass("hover");
    });

    $("#nextArea").click(function () {
        if (NextPicture != null) {
            Popup_ShowPictures_HideBox(NextPicture, NextBox);
        }
    });

    $("#previousArea").click(function () {
        if (PreviousPicture != null) {
            Popup_ShowPictures_HideBox(PreviousPicture, PreviousBox);
        }
    });

    document.getElementById('nextArea').addEventListener("touchstart", function () {
        OnMouseInArea(document.getElementById("nextButton"));
    });

    document.getElementById('nextArea').addEventListener("touchend", function () {
        OnMouseOutArea(document.getElementById("nextButton"));
    });

    document.getElementById('previousArea').addEventListener("touchstart", function () {
        OnMouseInArea(document.getElementById("previousButton"));
    });

    document.getElementById('previousArea').addEventListener("touchend", function () {
        OnMouseOutArea(document.getElementById("previousButton"));
    });

    //$('#previousArea').onmousemove = function () {
    //    OnMouseInArea(PreviousButton);
    //}
    //$('#previousArea').TouchStart = function () {
    //    OnMouseInArea(PreviousButton);
    //}
    //$('#previousArea').TouchEnd = function () {
    //    OnMouseOutArea(PreviousButton);
    //}

});

var PreviousPicture;
var NextPicture;
var PreviousBox;
var NextBox;
var IsMaxScreen;
var TimeoutHideBoutonsCursor;
var XBegin;
var XEnd;
var XDiff;

function Popup_ShowPictures_HideBox(pictureToShow, boxToHide) {

    $('#popupPicture').show();
    $('#imagePopup').attr('src', pictureToShow.src);
    $('#caption').html(pictureToShow.alt)

    var previousPictureId = "pict" + (parseInt(pictureToShow.id.replace("pict", "")) - 1);
    var nextPictureId = "pict" + (parseInt(pictureToShow.id.replace("pict", "")) + 1);
    var previousBoxId = "box" + (parseInt(boxToHide.id.replace("box", "")) - 1);
    var nextBoxId = "box" + (parseInt(boxToHide.id.replace("box", "")) + 1);
    //TODO : trouver le nombre dans la string et prendre -1 et +1 pour previous et next

    PreviousPicture = document.getElementById(previousPictureId);
    NextPicture = document.getElementById(nextPictureId);
    PreviousBox = document.getElementById(previousBoxId);
    NextBox = document.getElementById(nextBoxId);

    if (PreviousPicture != null) {
        $('#previousButton').show();
        $('#previoustArea').show();
    }
    else {
        $('#previousButton').hide();
        $('#previousArea').hide();
    }

    if (NextPicture != null) {
        $('#nextButton').show();
        $('#nextArea').show();
    }
    else {
        $('#nextButton').hide();
        $('#nextArea').hide();
    }


    ShowPicture_HideBox(pictureToShow, boxToHide);
}


function TouchMove() {
    XEnd = event.touches[0].clientX;
    XDiff = XEnd - XBegin;
    if (XDiff > 0 && PreviousPicture != null) {
        document.getElementById("imagePopup").style.transform = 'translate(' + XDiff + 'px)';
    }
    else if (XDiff < 0 && NextPicture != null) {
        document.getElementById("imagePopup").style.transform = 'translate(' + XDiff + 'px)';
    }
}

function TouchEnd() {
    if (XDiff > 40 && PreviousPicture != null) {
        $('#imagePopup').hide();
        Popup_ShowPictures_HideBox(PreviousPicture, PreviousBox);
        $('#imagePopup').show();
        document.getElementById("imagePopup").style.transform = 'translate(' + XDiff + 'px)';
    }
    else if (XDiff < -40 && NextPicture != null) {
        $('#imagePopup').hide();
        Popup_ShowPictures_HideBox(NextPicture, NextBox);
        $('#imagePopup').show();
        document.getElementById("imagePopup").style.transform = 'translate(' + XDiff + 'px)';
    }
    document.getElementById("imagePopup").style.transform = 'translate(0px)';
}

function TouchStart() {
    $('#previousButton').show();
    $('#nextButton').show();
    XBegin = event.touches[0].clientX;
    XDiff = 0;
}

function HideButtonsCursor() {
    $('#previousButton').hide();
    $('#nextButton').hide();
    $('#closeButton').hide();
    $('#maxScreenButton').hide();
    $('#defaultScreenButton').hide();
    $('#previousArea').css('cursor', 'none');
    $('#nextArea').css('cursor', 'none');
    $('#popupPicture').css('cursor', 'none');
}

function ShowButtonsCursor() {
    $('#previousArea').css('cursor', 'pointer');
    $('#nextArea').css('cursor', 'pointer');
    $('#popupPicture').css('cursor', 'default');
    $('#closeButton').show();
    if (PreviousPicture != null)
        $('#previousButton').show();
    if (NextPicture != null)
        $('#nextButton').show();
    if (IsMaxScreen)
        $('#defaultScreenButton').show();
    else
        $('#maxScreenButton').show();
    ScheduleHideButtonsCursor();
}

function ScheduleHideButtonsCursor() {
    clearTimeout(TimeoutHideBoutonsCursor);
    TimeoutHideBoutonsCursor = setTimeout(function () {
        HideButtonsCursor();
    }, 800);
}

function DefaultScreen() {
    $('#caption').show();
    $('#largeImageAndButtons').height('94%');
    $('#largeImageAndButtons').width('100%');
    $('#popupPicture').padding = '0.5%';
    $('#popupPicture').height('99%');
    $('#popupPicture').width('99%');
    $('#defaultScreenButton').hide();
    $('#maxScreenButton').show();
    IsMaxScreen = false;
    CloseFullScreen();
}

function MaxScreen() {
    $('#caption').hide();
    $('#largeImageAndButtons').height('100%');
    $('#largeImageAndButtons').width('100%');
    $('#popupPicture').padding = '0px';
    $('#popupPicture').width('100%');
    $('#popupPicture').height('100%');
    $('#maxScreenButton').hide();
    $('#defaultScreenButton').show();
    IsMaxScreen = true;
    OpenFullScreen();
}

function OpenFullScreen() {
    var largeImageAndButtons = document.getElementById("largeImageAndButtons");
    if (largeImageAndButtons.requestFullscreen) {
        largeImageAndButtons.requestFullscreen();
    } else if (largeImageAndButtons.mozRequestFullScreen) { /* Firefox */
        largeImageAndButtons.mozRequestFullScreen();
    } else if (largeImageAndButtons.webkitRequestFullscreen) { /* Chrome, Safari & Opera */
        largeImageAndButtons.webkitRequestFullscreen();
    } else if (largeImageAndButtons.msRequestFullscreen) { /* IE/Edge */
        largeImageAndButtons.msRequestFullscreen();
    }
}

function CloseFullScreen() {
    if (document.exitFullscreen) {
        document.exitFullscreen();
    } else if (document.mozCancelFullScreen) { /* Firefox */
        document.mozCancelFullScreen();
    } else if (document.webkitExitFullscreen) { /* Chrome, Safari and Opera */
        document.webkitExitFullscreen();
    } else if (document.msExitFullscreen) { /* IE/Edge */
        document.msExitFullscreen();
    }
}

function KeyDown() {
    if (event.keyCode == 37 && PreviousPicture != null) {
        Popup_ShowPictures_HideBox(PreviousPicture, PreviousBox);
        HideButtonsCursor();
    }
    if (event.keyCode == 39 && NextPicture != null) {
        Popup_ShowPictures_HideBox(NextPicture, NextBox);
        HideButtonsCursor();
    }
    if (event.code == "F11") {
        if (IsMaxScreen)
            DefaultScreen();
        else
            MaxScreen();
    }
}

function ShowPicture_HideBox(pictureToShowPermanently, boxToHidePermanently) {
    $(pictureToShowPermanently).show();
    $(boxToHidePermanently).hide();
}

function HideButtonsCursor() {
    $('#previousButton').hide();
    $('#nextButton').hide();
    $('#closeButton').hide();
    $('#maxScreenButton').hide();
    $('#defaultScreenButton').hide();
    $('#popupPicture').css('cursor', 'none');
    $('#previousArea').hide();
    $('#nextArea').hide();
}

