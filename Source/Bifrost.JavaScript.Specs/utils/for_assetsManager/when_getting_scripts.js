describe("when getting scripts", function () {
    var extension = "";
    var scripts = ["something.js", "something_else.js"];
    var scriptsReturned = [];
    var promiseCalled = false;
    var nameSpaceInitializedStub;

    beforeEach(function () {
        nameSpaceInitializedStub = sinon.stub();

        var assetsManager = Bifrost.assetsManager.createWithoutScope();

        assetsManager.scripts = undefined;
        Bifrost.namespaces = Bifrost.namespaces || {};
        Bifrost.namespaces.create = function () { return { initialize: nameSpaceInitializedStub }; };
        sinon.stub($, "get", function (url, parameters, callback) {
            extension = parameters.extension;
            callback(scripts);
        });

        assetsManager.initialize().continueWith(function () {
            promiseCalled = true;
        });

        scriptsReturned = assetsManager.getScripts();
    });

    afterEach(function () {
        $.get.restore();
    });

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