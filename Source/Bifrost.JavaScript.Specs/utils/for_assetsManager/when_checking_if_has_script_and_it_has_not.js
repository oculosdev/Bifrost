describe("when checking if has script and it has not", function () {
    var result = false;
    var server = {};
    var assetsManager = Bifrost.assetsManager.createWithoutScope({ server: server });
    assetsManager.scripts = ["something.js", "thestuff.js"];
    var result = assetsManager.hasScript("missing.js");

    it("should return that it has it", function () {
        expect(result).toBe(false);
    });
});