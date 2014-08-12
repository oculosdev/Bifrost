describe("when visiting a tag with two namespaces", function() {
    var objectModelManager = {
        canResolve: sinon.stub().returns(false),
    };

	var visitor = Bifrost.markup.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}
	});

	var exception = null;
	try {
	    var element = { localName: "ns:ns2:something", attributes: [], isKnownType: sinon.stub().returns(false) };
		visitor.visit(element);
	} catch( e ) {
		exception = e;
	}

	it("should throw multiple namespaces in name not allowed", function() {
	    expect(exception instanceof Bifrost.markup.MultipleNamespacesInNameNotAllowed).toBe(true);
	});
});