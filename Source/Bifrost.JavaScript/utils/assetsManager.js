Bifrost.namespace("Bifrost", {
    assetsManager: Bifrost.Singleton(function(server) {
        
        var self = this;
        this.scripts = [];
        this.pages = [];


        this.initialize = function () {
            var promise = Bifrost.execution.Promise.create();
            if (typeof self.scripts === "undefined" ||
                self.scripts.length == 0) {
                
                server.get("/Bifrost/AssetsManager", { extension: "js" }).continueWith(function (result) {
                    self.scripts = result;
                    Bifrost.namespaces.create().initialize();

                    server.get("/Bifrost/AssetsManager", { extension: "html" }).continueWith(function (result) {
                        self.pages = result;

                        promise.signal();
                    });
                });
            } else {
                promise.signal();
            }
            return promise;
        };

        this.initializeFromAssets = function (assets) {
            assets.forEach(function (asset) {
                var path = Bifrost.Path.create({ fullPath: asset });
                var extension = path.extension.toLowerCase();
                if (extension == ".js") {
                    self.scripts.push(asset);
                }
                if (extension == ".html") {
                    self.pages.push(asset);
                }
            });
            Bifrost.namespaces.create().initialize();
        };

        this.getScripts = function () {
            return self.scripts;
        };

        this.hasScript = function(script) {
            var found = false;
            self.scripts.some(function (scriptInSystem) {
                if (scriptInSystem === script) {
                    found = true;
                    return;
                }
            });

            return found;
        };

        this.getScriptPaths = function () {
            var paths = [];

            self.scripts.forEach(function (fullPath) {
                var path = Bifrost.Path.getPathWithoutFilename(fullPath);
                if (paths.indexOf(path) == -1) {
                    paths.push(path);
                }
            });
            return paths;
        };
    })
});