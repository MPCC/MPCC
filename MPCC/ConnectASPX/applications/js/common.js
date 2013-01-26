define([],
    function() {

        function to_html(template, context) {
            var newContext = $.extend({ }, context);
            return Mustache.to_html(template, newContext);
        }

    });