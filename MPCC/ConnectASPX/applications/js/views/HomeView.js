define([
    'jquery',
    'underscore',
    'backbone',
    'mustache',
    'text!templates/homeTemplate.html',
    'views/notificationsview',
    'views/sermonsview',
    'views/resourcesview',
    'views/newsview',
    'views/appsview'
], function ($, _, Backbone, Mustache, homeTemplate, NotificationsView, SermonsView, ResourcesView, NewsView, AppsView) {
    return Backbone.View.extend({
        el:$("#content"),
        render:function () {
            this.$el.html(homeTemplate);

            this.notificationView = new NotificationsView({
                el:$('#notifications'),
                router:this,
                model:null
            });

            this.newsView = new NewsView({
                el:$('#news'),
                router:this,
                model:null
            });
            this.appsView = new AppsView({
                el:$('#apps'),
                router:this,
                model:null
            });
            this.resourcesView = new ResourcesView({
                el:$('#resources'),
                router:this,
                model:null
            });
            this.sermonView = new SermonsView({
                el:$('#sermon'),
                router:this,
                model:null
            });
            return this;
        }
    });
});
