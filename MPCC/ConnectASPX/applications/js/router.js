define([
    'jquery',
    'underscore',
    'backbone',
    'views/HomeView',
    'views/notificationsview',
    'views/sermonsview',
    'views/newsview',
    'views/resourcesview',
    'views/appsview',
    'views/FooterView'
], function(
    $,
    _,
    Backbone,
    HomeView,
    NotificationsView,
    SermonsView,
    NewsView,
    ResourcesView,
    AppsView,
    FooterView
) {
    return Backbone.Router.extend({
        routes: {
            ':view': 'switchPage',
            '*actions': '_defaultAction'
        },
        initialize: function(){
           Backbone.history.start();
        },
        _defaultAction: function(actions){
            $('.mainnav li').removeAttr('class');
            $('.mainnav li').eq(0).addClass('active');
            var homeView = new HomeView();
            homeView.render();
            var footerView = new FooterView();
        }
        , switchPage: function(view, model) {
            model = model || null;
            if (this.currentView) {
                // give the view a chance to clean up.
                if (_.isFunction(this.currentView.destroy)) {
                    this.currentView.destroy();
                }
                $('#content').empty();
            }
            this.currentViewName = view;
            $('.mainnav li').removeAttr('class');
            var $row = $('<div/>',{'class':'row'});
            var $span = $('<div/>',{'class':'span12 '});
            var $widget= $('<div/>',{id:'viewcontent','class':'widget'});
            $span.html($widget)
            $row.html($span);
            $('#content').html($row);
            switch(view) {
                case '':
                    $('.mainnav li').eq(0).addClass('active');
                    this.navigate('overview', false);
                    this.currentViewName = 'overview';
                    this.loadView(DashboardView, model);
                    break;
                case 'news':

                    $('.mainnav li').eq(1).addClass('active');
                    this.newsView = new NewsView({
                        el:$('#viewcontent'),
                        router:this,
                        model:null
                    });
                    break;
                case 'resources':
                    $('.mainnav li').eq(3).addClass('active');
                    this.resourcesView = new ResourcesView({
                        el:$('#viewcontent'),
                        router:this,
                        model:null
                    });
                    break;
                case 'sermons':
                    $('.mainnav li').eq(2).addClass('active');
                    this.sermonView = new SermonsView({
                        el:$('#content'),
                        router:this,
                        model:null
                    });
                    break;
                case 'apps':
                    $('.mainnav li').eq(4).addClass('active');
                    this.appsView = new AppsView({
                        el:$('#viewcontent'),
                        router:this,
                        model:null
                    });
                    break;
                case 'notifications':
                    $('.mainnav li').eq(5).addClass('active');
                    this.notificationView = new NotificationsView({
                        el:$('#viewcontent'),
                        router:this,
                        model:null
                    });
                    break;
                default:
                    $('#content').html('404: View Not Found.');
            }
        }
    });
});
