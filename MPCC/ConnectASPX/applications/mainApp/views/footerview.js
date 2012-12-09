define(
    [
        'backbone',
        'text!../../../mainApp/templates/footer.html', 
        'mustache'
    ],
    function(
        Backbone,
        FooterTemplate, 
        Mustache
    ) {
        return Backbone.View.extend({
            initialize: function() {
                _.bindAll(this);

                this.render();
                return this;
            },

            destroy: function() {
                $(this.el).html('');
                return this;
            },

            render: function() {
                var self = this;
                $(this.el).html(Mustache.to_html(FooterTemplate, {}));
                return this;
            }
        });
    });