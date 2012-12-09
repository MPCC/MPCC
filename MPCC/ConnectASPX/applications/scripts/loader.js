define(['order!//cdnjs.cloudflare.com/ajax/libs/jquery/1.8.1/jquery.min',
        'order!//cdnjs.cloudflare.com/ajax/libs/underscore.js/1.3.3/underscore-min',
        'order!//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/2.1.1/bootstrap.min',
        'order!//cdnjs.cloudflare.com/ajax/libs/backbone.js/0.9.2/backbone-min'],
function () {
    return {
        Backbone: Backbone.noConflict(),
        Bootstrap: Bootstrap.noConflict(),
        _: _.noConflict(),
        $: jQuery.noConflict()
    };
});