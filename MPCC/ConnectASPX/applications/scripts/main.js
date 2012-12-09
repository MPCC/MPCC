//require.config({
//    paths: {
//        loader: 'loader',
//        jQuery: 'libs/jquery',
//        Bootstrap: 'libs/bootstrap', 
//        Underscore: 'libs/underscore',
//        Backbone: 'libs/backbone',
//        templates:'../templates'
//    }
//});
//require(['app'], function (App) {
//    App.initialize();
//});

requirejs.config({
    paths: {
        'loader': 'loader',
        'jquery': '//cdnjs.cloudflare.com/ajax/libs/jquery/1.8.1/jquery.min',
        'underscore': '//cdnjs.cloudflare.com/ajax/libs/underscore.js/1.3.3/underscore-min',
        'bootstrap': '//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/2.1.1/bootstrap.min',
        'backbone': '//cdnjs.cloudflare.com/ajax/libs/backbone.js/0.9.2/backbone-min',
        'mustache': '//cdnjs.cloudflare.com/ajax/libs/mustache.js/0.5.0-dev/mustache.min',
        'handlebars': '//cdnjs.cloudflare.com/ajax/libs/handlebars.js/1.0.rc.1/handlebars.min',
        'tmpl': 'vendor/tmpl',
        'date': '//cdnjs.cloudflare.com/ajax/libs/datejs/1.0/date.min'
    },
    shim: {
        'underscore': {
            exports: '_'
        }
        , 'backbone': {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        }
        , 'mustache': {
            exports: 'Mustache'
        }
        , 'handlebars': {
            exports: 'Handlebars'
        }
        , 'date': {
            exports: 'Date'
        }
    }
});
require(['app'], function (App) {
    // App.initialize();
    this.App = new App();
    //var x = new App();
});
