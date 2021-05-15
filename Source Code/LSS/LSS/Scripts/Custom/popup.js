$(function (){
    var PlaceHolderElement = $('#PlaceHolderHere');
    $('button[data-toggle="ajax-model"]').click(function (event) {
        var url = $this(this).data('url');
        var decodedURL = decodeURIComponent(url);
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parent('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolderElement.find(',modal').modal('hide');
        })
    }


})