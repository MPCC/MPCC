define(
    [
        'backbone',
        'text!templates/news.html',
        'mustache',
        'common'
    ],
    function (
        Backbone,
        NewsTemplate,
        Mustache,
        Common
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
                $(this.el).html(Mustache.to_html(NewsTemplate, {}));
                return this;
            }
        });
    });