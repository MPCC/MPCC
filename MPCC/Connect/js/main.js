require.config({
    paths: {
        loader: 'loader',
        jQuery: 'libs/jquery',
        Bootstrap: 'libs/bootstrap', 
        Underscore: 'libs/underscore',
        Backbone: 'libs/backbone',
        templates:'../templates'
    }
});
require(['app'], function (App) {
    App.initialize();
});