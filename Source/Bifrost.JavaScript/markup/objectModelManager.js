Bifrost.namespace("Bifrost.markup", {
	objectModelManager: Bifrost.Singleton(function(assetsManager) {
		var self = this;

		this.canResolve = function (name, namespace) {
		    // Map the namespace to path
            // Is there a JS file for the name in that namespace - if so, return true, if not - don't bother

		    return false;
		};

		this.beginResolve = function (name, namespace) {
		    var promise = Bifrost.execution.Promise.create();

		    // Map the namespace to path
		    // Add JS file to array for loading
		    // AssetsManager: If there is a HTML file, add it for loading if its not already loaded
		    // AssetsManager: If there is a CSS file, add it for loading

		    // Execute FileLoadTask - region... 

            // When loaded - if not of Element type - throw exception

		    return promise;
		};
	})
});