describe("when initializing", function () {
    var extension = "";
    var server = {
        get: sinon.stub().returns({ 
            continueWith: function () { } }
        )
    };

    Bifrost.assetsManager.scripts = undefined;
    Bifrost.namespaces = Bifrost.namespaces || {};
    Bifrost.namespaces.initialize = sinon.stub();
    var assetsManager = Bifrost.assetsManager.createWithoutScope({ server: server });
    assetsManager.initialize();

    it("should call server to get assets", function () {
        expect(server.get.calledWith("/Bifrost/AssetsManager",{ extension: "js" })).toBe(true);
    });
});