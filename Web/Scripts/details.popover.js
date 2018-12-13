
//Script trouvé sur internet, sur https://maxalley.wordpress.com/2014/08/19/bootstrap-3-popover-with-html-content/
$(function () {
    $('[rel="popover"]').popover({
        container: 'body',
        html: true,
        content: function () {
            var clone = $($(this).data('popover-content')).clone(true).removeClass('hide');
            return clone;
        }
    }).click(function (e) {
        e.preventDefault();
    });
});