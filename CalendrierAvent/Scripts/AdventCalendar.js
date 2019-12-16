// for Modifier.cshtml
function changePicture(input, imageIdToChange) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(imageIdToChange)
                .attr('src', e.target.result)
                .width(300);
        };
        reader.readAsDataURL(input.files[0]);
    }
}


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
        KeyDown(event.code);
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
});

var PreviousPicture;
var NextPicture;
var PreviousBox;
var NextBox;
var TimeoutHideBoutonsCursor;
var XBegin;
var XEnd;
var XDiff;

function FirstShowPopup(pictureToShow, boxToHide) {
    document.getElementById('popupPicture').style.animationName = 'zoom';
    document.getElementById('popupPicture').style.animationDuration = '0.5s';
    document.getElementById('popupPicture').style.webkitAnimationName = 'zoom';
    document.getElementById('popupPicture').style.webkitAnimationDuration = '0.5s';
    Popup_ShowPictures_HideBox(pictureToShow, boxToHide);
    //TODO JQUERY
}

function Popup_ShowPictures_HideBox(pictureToShow, boxToHide) {
    $('#popupPicture').show();
    $('#imagePopup').attr('src', pictureToShow.src);
    $('#caption').html(pictureToShow.alt)
    ShowButtonsCursor();

    var previousPictureId = getPreviousId(pictureToShow.id)
    var nextPictureId = getNextId(pictureToShow.id)
    var previousBoxId = getPreviousId(boxToHide.id)
    var nextBoxId = getNextId(boxToHide.id)
    PreviousPicture = document.getElementById(previousPictureId);
    NextPicture = document.getElementById(nextPictureId);
    PreviousBox = document.getElementById(previousBoxId);
    NextBox = document.getElementById(nextBoxId);

    if (PreviousPicture != null) {
        $('#previousButton').show();
        $('#previousArea').show();
        $('#previousImagePopup').attr('src', PreviousPicture.src);
    }
    else {
        $('#previousButton').hide();
        $('#previousArea').hide();
    }

    if (NextPicture != null) {
        $('#nextButton').show();
        $('#nextArea').show();
        $('#nextImagePopup').attr('src', NextPicture.src);
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
    var screenWidth = $('#largeImageAndButtons').width();
    var previousWidth = $("#previousImagePopup").width();
    var nextWidth = $("#nextImagePopup").width();
    if (XDiff > 0 && PreviousPicture != null) {
        var xdiffPrevious = XDiff - 10 + screenWidth - previousWidth;
        $('#previousImagePopup').show();
        document.getElementById("imagePopup").style.transform = 'translate(' + XDiff + 'px)';
        document.getElementById("previousImagePopup").style.transform = 'translate(' + xdiffPrevious + 'px, -50%)';
    }
    else if (XDiff < 0 && NextPicture != null) {
        var xdiffNext = XDiff + 10 - screenWidth + nextWidth;
        $('#nextImagePopup').show();
        document.getElementById("imagePopup").style.transform = 'translate(' + XDiff + 'px)';
        document.getElementById("nextImagePopup").style.transform = 'translate(' + xdiffNext + 'px, -50%)';
    }
}

function TouchEnd() {
    var screenWidth = $('#largeImageAndButtons').width();
    var previousWidth = $("#previousImagePopup").width();
    var nextWidth = $("#nextImagePopup").width();

    if (XDiff > 80 && PreviousPicture != null) {
        $("#imagePopup").hide();
        $("#previousImagePopup").animate({
            left: (-XDiff + 10 - (screenWidth - previousWidth)/2) + "px"
        }, {
            duration: "fast",
            easing: "linear",
            complete: function () {
            swapImagePrevious();
            }
        });
    }
    else if (XDiff < -80 && NextPicture != null) {
        $("#imagePopup").hide();
        $("#nextImagePopup").animate({
            right: (XDiff + 10 - (screenWidth - nextWidth) / 2) + "px"
        }, {
            duration: "fast",
            easing: "linear",
                complete: function () {
                    swapImageNext();
            }
        });
    }
    else {
         resetImagesPopup();
    }
}


function swapImageNext() {
    Popup_ShowPictures_HideBox(NextPicture, NextBox);
    $("#imagePopup").show();
    resetImagesPopup();
}

function swapImagePrevious() {
    Popup_ShowPictures_HideBox(PreviousPicture, PreviousBox);
    $("#imagePopup").show();
    resetImagesPopup();
}

function resetImagesPopup() {
    $('#nextImagePopup').hide();
    $('#previousImagePopup').hide();
    document.getElementById("imagePopup").style.transform = 'translate(0px)';
    document.getElementById("previousImagePopup").style.transform = 'translate(0px,0px)';
    document.getElementById("nextImagePopup").style.transform = 'translate(0px,0px)';
    document.getElementById("nextImagePopup").style.right = '-100%';
    document.getElementById("nextImagePopup").style.top = '50%';
    document.getElementById("previousImagePopup").style.left = '-100%';
    document.getElementById("previousImagePopup").style.top = '50%';
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
    if (PreviousPicture != null) {
        $("#previousArea").hover(function () {
            $('#previousButton').addClass("hover");
        }, function () {
            $('#previousButton').removeClass("hover");
        });
        $('#previousButton').show();
    }
    if (NextPicture != null) {
        $("#nextArea").hover(function () {
            $('#nextButton').addClass("hover");
        }, function () {
            $('#nextButton').removeClass("hover");
        });
        $('#nextButton').show();
    }
    if (document.fullscreenElement)
        $('#defaultScreenButton').show();
    else
        $('#maxScreenButton').show();
    ScheduleHideButtonsCursor();
}

function ScheduleHideButtonsCursor() {
    clearTimeout(TimeoutHideBoutonsCursor);
    TimeoutHideBoutonsCursor = setTimeout(function () {
        HideButtonsCursor();
    }, 1000);
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

function MaxScreen() {
    $('#caption').hide();
    $('#largeImageAndButtons').height('100%');
    $('#largeImageAndButtons').width('100%');
    $('#popupPicture').padding = '0px';
    $('#popupPicture').width('100%');
    $('#popupPicture').height('100%');
    $('#maxScreenButton').hide();
    $('#defaultScreenButton').show();
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

function KeyDown(keypress) {
    switch (keypress) {
        case 'ArrowLeft':
            if (PreviousPicture != null) {
                Popup_ShowPictures_HideBox(PreviousPicture, PreviousBox);
                HideButtonsCursor();
            }
            break;
        case 'ArrowRight':
            if (NextPicture != null) {
                Popup_ShowPictures_HideBox(NextPicture, NextBox);
                HideButtonsCursor();
            }
            break;
        case 'KeyF':
            if (document.fullscreenElement)
                DefaultScreen();
            else
                MaxScreen();
            break;
        case 'Escape':
            if (document.fullscreenElement)
                DefaultScreen();
            else {
                DefaultScreen();
                $('#popupPicture').hide();
            }
            break;
    }
}

function ShowPicture_HideBox(pictureToShowPermanently, boxToHidePermanently) {
    $(pictureToShowPermanently).show();
    $(boxToHidePermanently).hide();
}

function getPreviousId(string) {
    var indexDigit = string.search(/\d/);
    var prefix = string.substring(0, indexDigit);
    var sufix = string.substring(indexDigit, string.length);
    var previousDigit = parseInt(sufix, 10) - 1;
    return prefix + previousDigit;
}

function getNextId(string) {
    var indexDigit = string.search(/\d/);
    var prefix = string.substring(0, indexDigit);
    var sufix = string.substring(indexDigit, string.length);
    var nextDigit = parseInt(sufix, 10) + 1;
    return prefix + nextDigit;
}