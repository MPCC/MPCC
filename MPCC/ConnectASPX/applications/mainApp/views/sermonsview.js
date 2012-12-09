define(
    [
        'backbone',
        'text!../../../mainApp/templates/sermons.html',
        'mustache',
        'common'
    ],
    function (
        Backbone,
        SermonsTemplate,
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
                $(this.el).html(Mustache.to_html(SermonsTemplate, { videourl: "http://player.vimeo.com/video/54643545" }));
                return this;
            }
        });
    });