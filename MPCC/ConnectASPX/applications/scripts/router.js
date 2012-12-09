define([
     'Backbone'
], function (
    Backbone
) {
    return Backbone.Router.extend({
        last: []
        , currentID: null

        , routes: {
            'from/*last|*route': '_saveFrom'
            , 'edit/:id': '_edit'
            , 'copy/:id': '_copy'
            , 'create/*defaults': '_createWithDefaults'
            , 'requests/*filters': '_requestsWithFilter'
            , 'available/*filters': '_availableWithFilter'
            , ':view': 'switchPage'
        }

        /**
        * Initializes the Router.
        */
        , initialize: function () {
            var self = this;

            this.headerView = new HeaderView({
                el: $('#csHeader')
                , router: this
                , model: null
            });

            this.footerView = new FooterView({
                el: $('#csFooter')
                , router: this
                , model: null
            });

            this.init(function () {
                // Load the configs
                Config.loadSetConfig(self.config.Default);
            });

            // keep track of the current view.
            this.currentView = null;
        }

        , _saveFrom: function (last, route) {
            this.last.push(last);
            this.navigate(route, { trigger: true, replace: true });
        }

        /**
        * Switches the Main View bound to #contentScheduler
        *
        * @param {string} page A defined view name.
        */
        , switchPage: function (view, model) {
            model = model || null;
            if (this.currentView) {
                // give the view a chance to clean up.
                if (_.isFunction(this.currentView.destroy)) {
                    this.currentView.destroy();
                }
                $('#contentScheduler').empty();
            }
            this.currentViewName = view;
            switch (view) {
                case '':
                    this.navigate('overview', false);
                    this.currentViewName = 'overview';
                    this.loadView(DashboardView, model);
                    break;
                case 'overview':
                    this.loadView(DashboardView, model);
                    break;
                case 'requests':
                    this.loadView(RequestsView, model);
                    break;
                case 'available':
                    this.loadView(AvailableView, model);
                    break;
                case 'create':
                    this.loadView(EditView, model);
                    break;
                case 'edit':
                    this.loadView(EditView, model);
                    break;
                case 'copy':
                    this.loadView(EditView, model);
                    break;
                case 'config':
                    this.loadView(ConfigView, model);
                    break;
                case 'users':
                    this.loadView(UsersView, model);
                    break;
                default:
                    $('#contentScheduler').html('404: View Not Found.');
            }
        }

        , _edit: function (id) {
            var model = new Backbone.Model();
            model.set({ GUID: id, action: 'Edit' });

            this.switchPage('edit', model);
        }

        , _copy: function (id) {
            var model = new Backbone.Model();
            model.set({ GUID: id, action: 'Copy' });

            this.switchPage('copy', model);
        }

        , _createWithDefaults: function (defaults) {
            var model = new Backbone.Model()
                , defaultProperties = {}
                , parsed = defaults.split('/')
                , parsed_len = parsed.length
            ;
            // Puts parsed information into defaultProperties Object
            defaultProperties['time'] = parseInt(parsed[0]);

            // Puts filters from parsed info. into default Properties
            for (var i = 1; i < parsed_len - 1; i++) {
                defaultProperties['fk' + i] = parsed[i];
            }
            defaultProperties['zone_id'] = parsed[parsed_len - 1];
            model.set({ defaultProperties: defaultProperties });

            this.switchPage('create', model);
        }

        , _requestsWithFilter: function (filters) {
            var model = new Backbone.Model()
                , defaultFilter = {}
                , parsed = filters.split('/')
                , parsed_len = parsed.length
            ;
            defaultFilter['from'] = parseInt(parsed[0]);
            defaultFilter['to'] = parseInt(parsed[1]);

            for (var i = 2; i < parsed_len - 2; i++) {
                defaultFilter['fk' + (i - 1)] = parsed[i];
            }
            defaultFilter['status'] = parsed[parsed_len - 2];
            defaultFilter['zone'] = parsed[parsed_len - 1];
            model.set({ defaultFilter: defaultFilter });


            this.switchPage('requests', model);
        }

        , _availableWithFilter: function (filters) {
            var model = new Backbone.Model()
                , defaultFilter = {}
                , parsed = filters.split('/')
                , parsed_len = parsed.length
            ;
            defaultFilter['from'] = parseInt(parsed[0]);
            defaultFilter['to'] = parseInt(parsed[1]);

            for (var i = 2; i < parsed_len - 1; i++) {
                defaultFilter['fk' + (i - 1)] = parsed[i];
            }
            defaultFilter['state'] = parsed[parsed_len - 1];
            model.set({ defaultFilter: defaultFilter });

            this.switchPage('available', model);
        }

        // Pull back the application settings that won't change (much)
        , init: function (callback) {
            var self = this;
            if (self.config) {
                if (_.isFunction(callback)) {
                    callback(self.config);
                }
            } else {
                Fuel.ajax({
                    url: 'rest/init'
                    , dataType: 'json'
                    , success: function (r) {
                        self.config = r.config;
                        self.currentID = r.config.Default;
                        if (_.isFunction(callback)) {
                            callback(r.config);
                        }
                    }
                    , error: function () {
                        //TODO: something on error
                    }
                });
            }
        }

        , changeZoneSet: function (id) {
            this.currentID = id;
            this.reload();
        }

        , reload: function () {
            var x = Backbone.history.fragment;
            this.navigate('', { trigger: false });
            this.navigate(x, { trigger: true, replace: true });
        }

        // Make sure the configs are loaded (should cache) and load the configview if needed.
        // Else load the view that was requested.
        , loadView: function (view, model) {
            var self = this;
            this.init(function () {
                Config.loadSetConfig(self.currentID, function (c) {
                    self.headerView = new HeaderView({
                        el: $('#csHeader')
                        , router: self
                        , model: null
                        , config: {
                            Master: self.config
                            , Current: c
                        }
                        , page: self.currentViewName
                    });
                    if (!c.setupComplete) {
                        self.navigate('config', false);
                        self.currentView = new ConfigView({
                            el: $('#contentScheduler'),
                            router: self,
                            model: null,
                            config: {
                                Master: self.config
                                , Current: c
                            },
                            id: ''
                        });
                    } else {
                        self.currentView = new view({
                            el: $('#contentScheduler'),
                            router: self,
                            model: model,
                            mConfig: self.config,
                            config: {
                                Master: self.config
                                , Current: c
                            },
                            id: ''
                        });
                    }
                });
            });
        }
    });
});
