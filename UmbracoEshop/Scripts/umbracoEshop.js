$(document).ready(function () {
    obsahKosika();
});


var _umbracoEshop_SessionId = "";

function nastavSessionId(sessionId) {
    _umbracoEshop_SessionId = sessionId;
}


function obsahKosika() {

    var param = _umbracoEshop_SessionId;

    $.ajax('/Umbraco/UmbracoEshop/QuoteApi/BasketInfo?id=' + param,
        {
            type: 'POST',
            success: function (data) {
                $('.header-basket-pcs').html(data.Pocet);
                $('.header-basket-price').html(data.Cena);
            }
        });
}

function pridatDoKosika(el) {
    var self = $(el);
    var param = self.data('vyrobok') + "|" + _umbracoEshop_SessionId;

    $.ajax('/Umbraco/UmbracoEshop/QuoteApi/AddProductToQuote?id=' + param,
        {
            type: 'POST',
            success: function (data) {
                alert(data);
                obsahKosika();
            }
        });
}