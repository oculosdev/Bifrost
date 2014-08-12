describe("when getting script paths and multiple scripts are in same path", function () {
    var self = this;
    var paths = [];

    var server = {
        get: function () {
            return {
                continueWith: function(callback) {
                    callback([
                        "Something/cool.js",
                        "Something/cooler.js",
                        "Else/cool.js",
                        "Else/cooler.js"
                    ]);

                }
            }
        }
    };

    Bifrost.path = {
        getPathWithoutFilename: function (path) {
            if (path.indexOf("Something") == 0) {
                return "Something";
            }

            if (path.indexOf("Else") == 0) {
                return "Else";
            }
        }
    }

    Bifrost.namespaces = Bifrost.namespaces || {};
    Bifrost.namespaces.initialize = sinon.stub();

    var assetsManager = Bifrost.assetsManager.createWithoutScope({ server: server });
    assetsManager.initialize();
    paths = assetsManager.getScriptPaths();

    this.getCountFor = function (path) {
        var count = 0;

        for (var index = 0; index < paths.length; index++) {
            if (paths[index].indexOf(path) == 0) {
                count++;
            }
        }
        return count;
    };


    it("should return first path only once", function () {
        expect(self.getCountFor("Something")).toBe(1);
    });

    it("should return second path only once", function () {
        expect(self.getCountFor("Else")).toBe(1);
    });
});