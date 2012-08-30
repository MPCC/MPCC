require.config({
    paths: {
        'underscore': 'underscore',
        'backbone': 'backbone'
    }
});
require(["jquery", "app"], function ($, app) {
    

    $(document).ajaxError(function(e, xhr, options) {
        switch (xhr.status) {
            case 401:
                document.location = 'index.html';
                break;
        }
    });

    //    $('body').ajaxComplete(function (event, xhr, opts) {
    //        if (opts.url != 'http://localhost/Rest2/sessionkeep-alive') {
    //            //session.ExtendSession();
    //        }
    //    });

});
