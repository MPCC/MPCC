define(
    [
        'backbone',
        'text!../../../mainApp/templates/notifications.html',
        'mustache'
    ],
    function (
        Backbone,
        NotificationTemplate, 
        Mustache
    ) {
        return Backbone.View.extend({
            initialize: function () {
                _.bindAll(this);

                this.render();
                return this;
            },

            destroy: function () {
                $(this.el).html('');
                return this;
            },

            render: function () {
                var self = this;
                $(this.el).html(Mustache.to_html(NotificationTemplate, {}));
                return this;
            }
        });
    });