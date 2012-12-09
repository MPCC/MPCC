define([
        'jquery',
        'bootstrap',
        'underscore',
        'backbone',
        'mustache',
        'date',
        'all.min',
        '../mainApp/views/headerview',
        '../mainApp/views/footerview',
        '../mainApp/views/notificationsview',
        '../mainApp/views/sermonsview',
        '../mainApp/views/newsview',
        '../mainApp/views/appsview',
        '../mainApp/views/resourcesview'
    ], function(
        $,
        Bootstrap,
        _,
        Backbone,
        Mustache,
        Date,
        Fuel,
        HeaderView, 
        FooterView,
        NotificationView,
        SermonView,
        NewsView,
        AppsView,
        ResourcesView
) {
        return Backbone.Router.extend({
            routes: {
                'from/*last|*route': '_saveFrom',
                'edit/:id': '_edit',
                'copy/:id': '_copy',
                'create/*defaults': '_createWithDefaults',
                'requests/*filters': '_requestsWithFilter',
                'available/*filters': '_availableWithFilter',
                ':view': 'switchPage'
            },
            initialize: function() {
                var self = this;
                //Backbone.history.start();
                this.headerView = new HeaderView({
                    el: $('#header'),
                    router: this,
                    model: null
                });

                this.notificationView = new NotificationView({
                    el: $('#notifications'),
                    router: this,
                    model: null
                });

                this.sermonView = new SermonView({
                    el: $('#sermon'),
                    router: this,
                    model: null
                });

                this.newsView = new NewsView({
                    el: $('#news'),
                    router: this,
                    model: null
                });

                this.appsView = new AppsView({
                    el: $('#apps'),
                    router: this,
                    model: null
                });

                this.resourcesView = new ResourcesView({
                    el: $('#resources'),
                    router: this,
                    model: null
                });
                
                this.footerView = new FooterView({
                    el: $('#footer'),
                    router: this,
                    model: null
                });
            }
        });
    });

