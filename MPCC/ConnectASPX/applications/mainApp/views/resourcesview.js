define(
    [
        'backbone',
        'text!../../../mainApp/templates/resources.html',
        'mustache',
        'common'
    ],
    function (
        Backbone,
        ResourcesTemplate,
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
                $(this.el).html(Mustache.to_html(ResourcesTemplate, {}));
                return this;
            }
        });
    });