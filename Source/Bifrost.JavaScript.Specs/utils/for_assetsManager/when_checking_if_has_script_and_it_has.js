describe("when checking if has script and it has", function () {
    var server = {};
    var assetsManager = Bifrost.assetsManager.createWithoutScope({ server: server });
    assetsManager.scripts = ["something.js", "thestuff.js"];
    var result = assetsManager.hasScript("thestuff.js");

    it("should return that it has it", function () {
        expect(result).toBe(true);
    });
});