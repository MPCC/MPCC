define(['order!libs/jquery', 'order!libs/bootstrap', 'order!libs/underscore', 'order!libs/backbone'],
function () {
    return {
        Backbone: Backbone.noConflict(),
        _: _.noConflict(),
        Bootstrap: Bootstrap.noConflict(),
        $: jQuery.noConflict()
    };
});