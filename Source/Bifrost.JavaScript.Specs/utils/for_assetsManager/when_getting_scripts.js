describe("when getting scripts", function () {
    var extension = "";
    var scripts = ["something.js", "something_else.js"];
    var scriptsReturned = [];
    var promiseCalled = false;
    var nameSpaceInitializedStub;

    nameSpaceInitializedStub = sinon.stub();

    var server = {
        get: function () {
            return {
                continueWith: function(callback) {
                    callback(scripts);
                }
            }
        }
    };
    
    Bifrost.namespaces = Bifrost.namespaces || {};
    Bifrost.namespaces.create = function () { return { initialize: nameSpaceInitializedStub }; };

    var assetsManager = Bifrost.assetsManager.createWithoutScope({ server: server });
    assetsManager.scripts = undefined;
    assetsManager.initialize().continueWith(function () {
        promiseCalled = true;
    });

    scriptsReturned = assetsManager.getScripts();

    it("should get scripts", function () {
        expect(scriptsReturned).toBe(scripts);
    });

    it("should initialize namespaces after scripts have been received", function () {
        expect(nameSpaceInitializedStub.called).toBe(true);
    });

    it("should signal the promise after scripts have been received", function () {
        expect(promiseCalled).toBe(true);
    });
});