define(
    [
        'backbone'
        , 'text!../../../mainApp/templates/header.html'
        , 'mustache'
    ],
    function (
        Backbone
        , HeaderTemplate
        , Mustache
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
                $(this.el).html(Mustache.to_html(HeaderTemplate, {}));
                return this;
            }
        });
    });