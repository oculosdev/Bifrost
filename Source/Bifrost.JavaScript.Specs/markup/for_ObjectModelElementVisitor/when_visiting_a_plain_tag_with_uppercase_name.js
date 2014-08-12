describe("when visiting a plain tag with uppercase name", function() {
	var objectModelManager = {
	    canResolve: sinon.stub().returns(true),
	    beginResolve: sinon.stub().returns({
	        continueWith: function (callback) {
	            
	        }
	    })
	};

	var visitor = Bifrost.markup.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}
	});

	var element = { localName: "SOMETHING", attributes: [], isKnownType: sinon.stub().returns(false) };
	visitor.visit(element);

	it("should ask for an object by tag name", function() {
	    expect(objectModelManager.beginResolve.calledWith("something")).toBe(true);
	});
});